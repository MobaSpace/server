using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobaSpace.Core.Data.Api
{
    public abstract class ApiService : IHostedService
    {
        #region Variables
        private const int SLEEP_TIME_MS = 60000;

        protected readonly IServiceScopeFactory _services;
        protected readonly ILogger<ApiService> _logger;
        protected readonly IHttpClientFactory _clientFactory;
        #endregion

        #region Constructeur
        public ApiService(IServiceScopeFactory services, ILogger<ApiService> logger, IHttpClientFactory clientFactory)
        {
            this._services = services;
            this._logger = logger;
            this._clientFactory = clientFactory;
        }
        #endregion

        #region Pilotage du service
        public async Task StartAsync(CancellationToken token)
        {
            this._logger.LogInformation($"Service {GetApiType()} api is starting.");
            await Task.Delay(1, token);
            _ = Task.Factory.StartNew(ServiceDoWorkAsync, token);
            return;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Service {GetApiType()} api is stopping.");
            return Task.CompletedTask;
        }
        #endregion

        #region Fonction génériques de travail avec l'api
        private async Task ServiceDoWorkAsync(object state)
        {
            var token = (CancellationToken)state;

            this._logger.LogInformation($"Service {GetApiType()} api started.");
            while (true)
            {
                using var scope = this._services.CreateScope();
                var scopedDatalayer = scope.ServiceProvider.GetService<MobaDataLayer>();
                //Get apis from db
                var apis = new OAuth2Api[0];
                try
                {
                    apis = scopedDatalayer.GetApis(GetApiType()).ToArray();
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, $"Fail go get {GetApiType()} apis");
                    goto wait;
                }

                //Iterate on api
                foreach (var api in apis)
                {
                    try
                    {
                        await scopedDatalayer.BeginTransactionAsync();
                        await WorkWithApiAsync(api, scopedDatalayer, token);
                        scopedDatalayer.CommitTransaction();
                        this._logger.LogInformation($"Successfully request api {api.Provider}");
                    }
                    catch (Exception ex)
                    {
                        scopedDatalayer.RollbackTransaction();
                        this._logger.LogError(ex, $"Fail to request api {api.Provider}");
                    }
                }

            wait:
                await Task.Delay(SLEEP_TIME_MS, token);//wait
                if(token.IsCancellationRequested)
                {
                    this._logger.LogInformation($"Service {GetApiType()} api stopped.");
                    break;//release task
                }
            }
        }
        private async Task WorkWithApiAsync(OAuth2Api api, MobaDataLayer datalayer, CancellationToken token)
        {
            await ManageDevicesAsync(api, datalayer, token);
        }
        public async Task ManageDevicesAsync(OAuth2Api api, MobaDataLayer datalayer, CancellationToken token)
        {
            using HttpClient client = _clientFactory.CreateClient();
            var devices = await GetDevicesAsync(api, client, token);
            if (devices != null)
            {
                await ManageDevicesAsync(api, devices, datalayer, _logger, token);
            }
        }
        public async static Task ManageDevicesAsync(OAuth2Api api, IEnumerable<IApiDevice> devices, MobaDataLayer datalayer, ILogger logger, CancellationToken token)
        {
            await AddDevicesAsync(api, devices, datalayer, logger);
            await DeleteDevicesAsync(api, devices, datalayer, logger, token);
        }
        public static async Task DeleteApiAsync(OAuth2Api api, MobaDataLayer datalayer, ILogger logger, CancellationToken token = default)
        {
            try
            {
                await DeleteDevicesAsync(api, new IApiDevice[0], datalayer, logger, token);
                await datalayer.DeleteApi(api);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Fail to delete api {api}");
            }
        }
        public static async Task AddDevicesAsync(OAuth2Api api, IEnumerable<IApiDevice> devices, MobaDataLayer datalayer, ILogger logger)
        {
            foreach (var device in devices)
            {
                Capteur capteur = await datalayer.GetCapteurByIdentifiantAsync(device.GetDeviceId());
                if (capteur is null)//le capteur n'existe pas on le créé
                {
                    capteur = new Capteur()
                    {
                        Marque = api.Provider,
                        Modele = device.GetDeviceModel(),
                        Type = device.GetDeviceType(),
                        Identifiant = device.GetDeviceId(),
                        Creation = DateTime.UtcNow,
                        Modification = DateTime.UtcNow,
                    };
                    await datalayer.CreateCapteurAsync(capteur);
                    logger.LogInformation($"Added new device {capteur} to db");
                }
                if (!await datalayer.ApiCapteurExists(api, capteur))//on créé la liaison au patient si elle n'existe pas
                {
                    capteur.ApisCapteurs.Add(new ApiCapteur() { Api = api, Capteur = capteur });
                    await datalayer.UpdateCapteurAsync(capteur);
                    logger.LogInformation($"Linked device {capteur} to api {api}");
                }
            }
        }
        public static async Task DeleteDevicesAsync(OAuth2Api api, IEnumerable<IApiDevice> devices, MobaDataLayer datalayer, ILogger logger, CancellationToken token = default)
        {
            for (int i = 0; i < api.Capteurs.Count(); i++)
            {
                var c = api.Capteurs.ElementAt(i);

                var capteur = await datalayer.GetCapteurAsync(c.Id, token);
                if (!devices.Any(d => d.GetDeviceId() == capteur.Identifiant))//ce capteur n'est pas présent das les devices api
                {
                    var pc = capteur.ApisCapteurs.Where(ac => ac.Api.Id == api.Id).First();
                    capteur.ApisCapteurs.Remove(pc);
                    if (capteur.ApisCapteurs.Count > 0)//Ce capteur est toujours lié à une api
                    {
                        await datalayer.UpdateCapteurAsync(capteur, token);
                        logger.LogInformation($"Unlink device {capteur} and {api}");
                    }
                    else//Ce capteur n'est plus lié à aucun patient
                    {
                        await datalayer.DeleteCapteurAsync(capteur, token);
                        logger.LogInformation($"Delete device {capteur} because it's not used anymore");
                    }
                }
            }
        }
        #endregion

        #region Fonctions abstraites
        protected abstract ApiType GetApiType();
        protected abstract Task<IEnumerable<IApiDevice>> GetDevicesAsync(OAuth2Api api, HttpClient client, CancellationToken token);
        #endregion
    }
}
