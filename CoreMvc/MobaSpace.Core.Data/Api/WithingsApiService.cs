using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MobaSpace.Core.Data.Api
{
    public class WithingsApiService : ApiService
    {
        public WithingsApiService(IServiceScopeFactory services, ILogger<WithingsApiService> logger, IHttpClientFactory clientFactory) 
            : base(services, logger, clientFactory)
        {

        }

        protected override ApiType GetApiType()
        {
            return ApiType.Withings;
        }

        protected override async Task<IEnumerable<IApiDevice>> GetDevicesAsync(OAuth2Api api, HttpClient client, CancellationToken token)
        {
            return await GetDevicesAsync(api, _logger, client, token);
        }

        public static async Task<IEnumerable<IApiDevice>> GetDevicesAsync(OAuth2Api api, ILogger logger, HttpClient client, CancellationToken token)
        {
            logger.LogInformation("Request api to get user devices");
            var request = new HttpRequestMessage(HttpMethod.Get, "https://wbsapi.withings.net/v2/user?action=getdevice");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", api.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request, token);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"Received error response code while requesting device for api {api.Provider}/{api.ApiUserId} : {response.StatusCode}");
                return null;
            }
            var withingsDevicesStr = await response.Content.ReadAsStringAsync();
            var withingsDevicesJson = JObject.Parse(withingsDevicesStr);

            //check status
            var status = withingsDevicesJson.Value<string>("status");
            if (status != "0")
            {
                logger.LogError($"Received invalid device status while requesting devices for api {api.Provider}/{api.ApiUserId} : {status}");
                return null;
            }
            //status is good deserialize 
            var devicesJson = withingsDevicesJson["body"]["devices"];
            var device = devicesJson.ToObject<IEnumerable<WithingsDevice>>();
            return device;
        }
    }
}
