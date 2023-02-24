using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Models;
using MobaSpace.Core.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AspNet.Security.OAuth.Fitbit;
using System.Threading.Tasks;
using System.IO;

namespace MobaSpace.Core.Data.Api
{
    public static class ApiExtension
    {
        public static AuthenticationBuilder AddWithings(this AuthenticationBuilder builder, string clientId, string secretId, string callbackPath,string authorizeEndPoint, string tokenEndPoint, string scope, string webServerUri)
        {
            ApiType API_TYPE = ApiType.Withings;
            return builder.AddOAuth("Api" + API_TYPE.ToString(), API_TYPE.ToString(), options =>
            {
                options.ClientId = clientId;
                options.ClientSecret = secretId;
                options.CallbackPath = PathString.FromUriComponent(callbackPath);
                options.AuthorizationEndpoint = authorizeEndPoint;
                options.TokenEndpoint = tokenEndPoint;
                options.SaveTokens = true;
                options.Scope.Add(scope);
                options.Events = new OAuthEvents
                {
                OnCreatingTicket = async context =>
                    {
                        var logger = LogUtils.CreateLogger<OAuth2ApiWithings>();
                        using (logger.BeginScope($"Creating ticket for {API_TYPE}"))
                        {
                            string userid = "";
                            try
                            {
                                userid = context.TokenResponse.Response.RootElement.GetString("userid");
                            }
                            catch(Exception ex)
                            {
                                logger.LogError(ex, $"Fail to get userid from {API_TYPE} ticket response");
                                goto err;
                            }
                            logger.LogInformation($"Received ticket for userId:{userid}");


                            //create api
                            var api = new OAuth2ApiWithings()
                            {
                                AccessToken = context.AccessToken,
                                RefreshToken = context.RefreshToken,
                                ExpirationDate = DateTime.UtcNow.Add(context.ExpiresIn.GetValueOrDefault()),
                                ApiUserId = userid,
                            };

                            //request attached devices
                            var request = new HttpRequestMessage(HttpMethod.Get, "https://wbsapi.withings.net/v2/user?action=getdevice");
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            logger.LogInformation("Request api to get user devices");
                            try
                            {
                                api.Devices = await WithingsApiService.GetDevicesAsync(api, logger, context.Backchannel, context.HttpContext.RequestAborted) as List<WithingsDevice>;
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Fail to parse withings devices reponse");
                                goto err;
                            }

                            //send informations to the OAuth2Apis controller
                            var uri = Path.Combine(webServerUri, "OAuth2Apis", "RegisterApiWithings");

                            var apiJson = JsonConvert.SerializeObject(api);
                            var content = new StringContent(apiJson, Encoding.UTF8, "application/json");
                            var response = await context.Backchannel.PostAsync(uri, content);
                            try
                            {
                                response.EnsureSuccessStatusCode();
                            }
                            catch(Exception ex)
                            {
                                logger.LogError(ex, "Fail to notify new api to the server");
                            }

                            return;//goto redirect uri
                        err:
                            if (context.Properties.Items.ContainsKey("status"))
                            {
                                context.Properties.Items["status"] = "1";
                            }
                        }
                    }
                };
            });

        }

        public static AuthenticationBuilder AddFitbit(this AuthenticationBuilder builder, string clientId, string secretId, string callbackPath, string webServerUri)
        {
            ApiType API_TYPE = ApiType.Fitbit;
            return builder.AddOAuth("Api" + API_TYPE.ToString(), API_TYPE.ToString(), options =>
            {
                options.ClientId = clientId;
                options.ClientSecret = secretId;
                options.CallbackPath = new PathString(callbackPath);
                options.AuthorizationEndpoint = "https://www.fitbit.com/oauth2/authorize?response_type=token";
                options.TokenEndpoint = "https://api.fitbit.com/oauth2/token";
                options.SaveTokens = true;
                options.Scope.Add("activity heartrate location nutrition profile settings sleep social weight");
                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var logger = LogUtils.CreateLogger<OAuth2ApiFitbit>();
                        using (logger.BeginScope($"Creating ticket for {API_TYPE}"))
                        {
                            string userid = "";
                            try
                            {
                                userid = context.TokenResponse.Response.RootElement.GetString("userid");
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Fail to get userid from {API_TYPE} ticket response");
                                goto err;
                            }
                            long patientId;
                            try
                            {
                                var idPatientStr = context.Properties.Items["idPatient"];
                                patientId = Convert.ToInt64(idPatientStr);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Fail to get idPatient from {API_TYPE} ticket response");
                                goto err;
                            }
                            logger.LogInformation($"Received ticket for userId:{userid} and patientId:{patientId}");


                            //create api
                            var api = new Models.OAuth2ApiFitbit()
                            {
                                AccessToken = context.AccessToken,
                                RefreshToken = context.RefreshToken,
                                ExpirationDate = DateTime.UtcNow.Add(context.ExpiresIn.GetValueOrDefault()),
                                ApiUserId = userid,
                                Patient = new PatientC() { Id = patientId }
                            };

                            logger.LogInformation("Request api to get user devices");
                            try
                            {
                                api.Devices = await FitbitApiService.GetDevicesAsync(api, logger, context.Backchannel, context.HttpContext.RequestAborted) as List<FitbitDevice>;
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Fail to parse fitbit devices reponse");
                                goto err;
                            }

                            //send informations to the OAuth2Apis controller
                            var uri = Path.Combine(webServerUri, "OAuth2Apis", "RegisterApiFitbit");

                            var apiJson = JsonConvert.SerializeObject(api);
                            var content = new StringContent(apiJson, Encoding.UTF8, "application/json");
                            var response = await context.Backchannel.PostAsync(uri, content);
                            try
                            {
                                response.EnsureSuccessStatusCode();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Fail to notify new api to the server");
                            }

                            return;//goto redirect uri
                        err:
                            if (context.Properties.Items.ContainsKey("status"))
                            {
                                context.Properties.Items["status"] = "1";
                            }
                        }
                    }
                };
            });

        }
    }
}
