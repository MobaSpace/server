using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using MobaSpace.Core.Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobaSpace.Core.Data.Api
{
    class FitbitApiService : ApiService
    {
        public FitbitApiService(IServiceScopeFactory services, ILogger<WithingsApiService> logger, IHttpClientFactory clientFactory)
            : base(services, logger, clientFactory)
        {

        }

        protected override ApiType GetApiType()
        {
            return ApiType.Fitbit;
        }

        protected override async Task<IEnumerable<IApiDevice>> GetDevicesAsync(OAuth2Api api, HttpClient client, CancellationToken token)
        {
            return await GetDevicesAsync(api, _logger, client, token);
        }

        public static async Task<IEnumerable<IApiDevice>> GetDevicesAsync(OAuth2Api api, ILogger logger, HttpClient client, CancellationToken token)
        {
            logger.LogInformation("Request api to get user devices");
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.fitbit.com/1/user/-/devices.json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", api.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request, token);
            if(!response.IsSuccessStatusCode)
            {
                logger.LogError($"Received error response code while requesting devices for api {api.Provider}/{api.ApiUserId} : {response.StatusCode}");
                return null;
            }
            var devicesStr = await response.Content.ReadAsStringAsync();
            var devicesJson = JObject.Parse(devicesStr);

            return devicesJson.ToObject<IEnumerable<FitbitDevice>>();
        }
    }
}
