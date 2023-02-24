using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using MobaSpace.Core.Data.Api;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using MobaSpace.Web.UI.Models;
using MobaSpace.Web.UI.ViewModels;
using MobaSpace.Core.Utils;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace MobaSpace.Web.UI.Controllers
{
    public static class GlobalVariables
    {
        public static string ApiName { get; set; }

        public static int nbApi { get; set; }

        public static string StatusMessage { get; set; }
    }
    [Authorize(Roles = Roles.Administrateur)]
    public class OAuth2ApisController : Controller
    {
        private readonly MobaDataLayer _datalayer;
        private readonly SignInManager<User> _signinManager;
        private readonly IOptions<List<ApiSettings>> _apiSettings;
        private readonly ILogger<OAuth2ApisController> _logger;
        private readonly IConfiguration _config;

        public OAuth2ApisController(MobaDataLayer dataLayer, SignInManager<User> signinManager, IOptions<List<ApiSettings>> apiSettings, ILogger<OAuth2ApisController> logger, IConfiguration config)
        {
            _datalayer = dataLayer;
            _signinManager = signinManager;
            _apiSettings = apiSettings;
            _logger = logger;
            _config = config;

        }

        public async Task<ActionResult> Index()
        {
            this.ViewBag.Apis = (await this._signinManager.GetExternalAuthenticationSchemesAsync()).Where(shm => shm.Name.Contains("Api")).ToList();
            this.ViewBag.StatusMessage = GlobalVariables.StatusMessage;
            GlobalVariables.StatusMessage = null;
            await _datalayer.BeginTransactionAsync();
            var ListApi = await _datalayer.GetApisAsync();
            _datalayer.CommitTransaction();
            return View(ListApi);
        }

        //Create a new API
        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddApi(string providerName, string name , long idTest )
        {
            GlobalVariables.ApiName = name;
            GlobalVariables.nbApi = _datalayer.GetApis().Count();
            GlobalVariables.StatusMessage = null;

            if (providerName == "ApiWithings" || providerName == "Withings")
            {
                var redirectUrl = _config.GetValue<string>("Apis:Withings:CallbackPath");
                var properties = _signinManager.ConfigureExternalAuthenticationProperties(providerName, redirectUrl);
                return Redirect("https://tokens.mobaspace.com/generate?clientID=" + _config.GetValue<string>("Apis:Withings:ClientId") + "&secretID=" + _config.GetValue<string>("Apis:Withings:ClientSecret") + "&redirectTo="+ HttpContext.Request.Scheme +"://" + HttpContext.Request.Host  );
            }
            else
            {
                var redirectUrl = _config.GetValue<string>("Apis:"+ providerName +":CallbackPath");
                var properties = _signinManager.ConfigureExternalAuthenticationProperties(providerName, redirectUrl);
                return new ChallengeResult(providerName, properties);
            }
        }

        [AllowAnonymous]
        public IActionResult AddApi(string page, string handler)
        {
            var Apis = _datalayer.GetApis();
            if (GlobalVariables.nbApi < Apis.Count())
            {
                GlobalVariables.StatusMessage = "Réussite : Une Api a été ajoutée ! ";
            }
            else if (GlobalVariables.StatusMessage is null)
            {
                GlobalVariables.StatusMessage = "Réussite : Une Api a été modifiée ! ";
            }
            return RedirectToAction(handler, page);
        }

        //Display trackers form
        [AllowAnonymous]
        public async Task<IActionResult> AddTrackers(string name , string providerName)
        {

            var api = await _datalayer.GetApisByNameAsync(name);
            if (api == null)
            {
                var vm = new OAuth2ApiViewModel();
                ViewData["NameApi"] = name;
                ViewData["Marque"] = providerName;
                return View(vm);
            }
            else
            {
                GlobalVariables.StatusMessage = "Erreur : le nom pour cette api tracker existe déjà ! ";
                return RedirectToAction(nameof(Index));
            }

        }

        //Create a new trackers
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrackers(OAuth2ApiViewModel vm)
        {
            try
            {

                await this._datalayer.BeginTransactionAsync();
                var api = new OAuth2Api();
                var capteur = new Capteur();
                var tracker = new Tracker();
                // create a new capteur
                if (!String.IsNullOrEmpty(vm.Capteur.Marque) && !String.IsNullOrEmpty(vm.Capteur.Identifiant))
                {
                    capteur.Marque = vm.Capteur.Marque;
                    capteur.Modele = vm.Capteur.Modele;
                    capteur.EtatOK = true;
                    capteur.Type = "Tracker";
                    capteur.Identifiant = vm.Capteur.Identifiant.ToUpper();
                    capteur.Creation = DateTime.UtcNow;
                    capteur.Modification = DateTime.UtcNow;
                    await this._datalayer.CreateCapteurAsync(capteur);
                }
                else
                {
                    throw new Exception();
                }


                var newCapteur = await this._datalayer.GetCapteurByIdentifiantAsync(vm.Capteur.Identifiant.ToUpper());
                if( newCapteur != null)
                {
                    // create a new api
                    api.ApiName = vm.Api.ApiName;
                    api.Provider = vm.Capteur.Marque;
                    api.Capteurs.Append(newCapteur);
                    await this._datalayer.AddApi(api);
                }
                else {
                    throw new Exception();
                }

                //link capteur Api 
               
                var newApi = await this._datalayer.GetApisByNameAsync(vm.Api.ApiName);
                if(newApi != null)
                {
                    newCapteur.ApisCapteurs.Add(new ApiCapteur() { Api = newApi, Capteur = newCapteur });
                    await _datalayer.UpdateCapteurAsync(capteur);
                }
                else
                {
                    throw new Exception();
                }


                // create a new tracker

                tracker.Capteur = newCapteur;
                tracker.LastUpdate = DateTime.UtcNow;
                tracker.Traite = false;

                await this._datalayer.AddTracker(tracker);
                this._datalayer.CommitTransaction();
                GlobalVariables.StatusMessage = "Réussite : Une Api a été ajoutée ! ";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("ControllerError", "Impossible de créer l'api, l'identifiant est surement déjà utilisé.");
                _logger.LogError(ex, "Fail to create API");
                this._datalayer.RollbackTransaction();
                return RedirectToAction(nameof(AddTrackers), new { name = vm.Api.ApiName , providerName = vm.Capteur.Marque });
            }
        }

        //Delete an API
        public async Task<IActionResult> Delete(long id)
        {
            var Api = await this._datalayer.GetApiAsync(id);
            if (Api is null)
            {
                return NotFound("idApi");
            }
            return View(Api);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OAuth2Api vm)
        {
            await this._datalayer.BeginTransactionAsync();
            var api = await _datalayer.GetApiAsync(vm.Id);
            if (api.Patient != null)
            {
                GlobalVariables.StatusMessage = "Erreur : L'api est lié à un résident ! ";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _datalayer.DeleteApiAsync(vm.Id);
                foreach (Capteur capteur in api.Capteurs)
                {
                    await _datalayer.DeleteCapteurAsync(capteur);
                }
            }
            this._datalayer.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }

        //Edit an API
        public async Task<IActionResult> Edit(long id)
        {
            var api = (await _datalayer.GetApisCapteursByIdApiAsync(id)).First();
            if(api is null)
            {
                return NotFound();
            }
            var vm = new OAuth2ApiViewModel(api);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OAuth2ApiViewModel vm)
        {
            var api = await _datalayer.GetApisByIdWithoutPatientAsync(vm.Api.Id);
            api.ApiName = vm.Api.ApiName;
            ModelState.Clear();
            TryValidateModel(api);
            if (ModelState.IsValid)
            {
                try
                {
                    await _datalayer.BeginTransactionAsync();
                    await _datalayer.UpdateApi(api);
                    if (api.Provider.Equals("Mobaspace"))
                    {
                        var capteur = (await _datalayer.GetApisCapteursByIdApiAsync(api.Id)).First().Capteur;
                        capteur.Identifiant = vm.Capteur.Identifiant;
                        await _datalayer.UpdateCapteurAsync(capteur);
                        var tracker = await _datalayer.GetTrackerAsyncByIdCapteur(capteur.Id);
                        tracker.LastUpdate = DateTime.UtcNow;
                        await this._datalayer.UpdateTracker(tracker);
                        GlobalVariables.StatusMessage = "Réussite : Une Api a été modifiée ! ";
                    }
                    _datalayer.CommitTransaction();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    GlobalVariables.StatusMessage = "Erreur : Impossible de modifier cette API dans la base de donnée !";
                    _datalayer.RollbackTransaction();
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                return View(vm);
            }

        }

        //Register a Fitbit API (not implemented)
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterApiFitbit([FromBody] OAuth2ApiFitbit api)
        {
            return await _RegisterApi(api);
        }

        private async Task<IActionResult> _RegisterApi(OAuth2Api api)
        {
            var apiSettings = _apiSettings.Value.FirstOrDefault(a => a.Provider == api.Provider);
            if(apiSettings is null)
            {
                return this.ValidationProblem();
            }

            api.ClientId = apiSettings.ClientId;
            api.ClientSecret = apiSettings.ClientSecret;
            ModelState.Clear();
            TryValidateModel(api);
            
            if (!ModelState.IsValid)
            {
                return this.ValidationProblem();
            }

            try
            {
                _logger.LogInformation($"Adding api {api.Provider} to db");
                if (_datalayer.ApiExists(api))
                {
                    IEnumerable<OAuth2Api> existingApis = this._datalayer.GetApis();
                    foreach (OAuth2Api existingApi in existingApis)
                    {
                        
                        if (existingApi.ApiUserId == api.ApiUserId && existingApi.Provider == api.Provider)
                        {
                            existingApi.RefreshToken = api.RefreshToken;
                            existingApi.AccessToken = api.AccessToken;
                            existingApi.ExpirationDate = api.ExpirationDate;
                            if (existingApi.ApiName != GlobalVariables.ApiName)
                            {
                                GlobalVariables.StatusMessage = "Erreur : Ce nom d'Api n'existe pas pour cette API !";
                                return this.ValidationProblem();
                            }
                            api = existingApi;
                        }
                    }
                    await this._datalayer.UpdateApi(api);
                    return Ok("L'api existe déjà dans la base, elle a donc été modifiée.");
                }
                var Apis = _datalayer.GetApis();
                foreach (OAuth2Api Api in Apis)
                {
                    if (Api.ApiName == GlobalVariables.ApiName)
                    {
                        GlobalVariables.StatusMessage = "Erreur : Ce nom d'Api existe déjà pour un autre API !";
                        return this.ValidationProblem();
                    }
                }
                api.ApiName = GlobalVariables.ApiName;

                if (api.ApiName is null) {
                    throw new Exception("Api name is null");
                }

                await _datalayer.BeginTransactionAsync();
                await this._datalayer.AddApi(api);
                switch (api)
                {
                    case OAuth2ApiWithings withingsApi:
                        await WithingsApiService.AddDevicesAsync(api, withingsApi.Devices, _datalayer, _logger);
                        break;
                    case OAuth2ApiFitbit fitbitApi:
                        throw new NotImplementedException();
                    default:
                        throw new NotImplementedException($"Add {api.Provider} api function is not implemented in the controller");
                }
                _datalayer.CommitTransaction();
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                GlobalVariables.StatusMessage = "Erreur : Impossible d'insérer cette API dans la base de donnée !";
                _datalayer.RollbackTransaction();
                return Problem();
            }
        }

        //Register and refresh WithingsAPI
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterWithingsApi(String userId, String refreshToken, String accessToken, String expires)
        {
            OAuth2ApiWithings api = new OAuth2ApiWithings();
            api.ClientId = _config.GetValue<string>("Apis:Withings:ClientId");
            api.ClientSecret = _config.GetValue<string>("Apis:Withings:ClientSecret");
            api.ApiUserId = userId;
            api.RefreshToken = refreshToken;
            api.AccessToken = accessToken;
            api.ExpirationDate = DateTime.Now.AddSeconds(double.Parse(expires));
            api.ApiName = GlobalVariables.ApiName;
            api.Provider = "Withings";

            var request = new HttpRequestMessage(HttpMethod.Get, "https://wbsapi.withings.net/v2/user?action=getdevice");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", api.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger.LogInformation("Request api to get user devices");
            HttpClient httpClient = new HttpClient();
            try
            {
                api.Devices = await WithingsApiService.GetDevicesAsync(api, _logger, httpClient, HttpContext.RequestAborted) as List<WithingsDevice>;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to parse withings devices reponse");
            }

            await _RegisterApi(api);

            return AddApi("OAuth2Apis", "");

        }
    }
}