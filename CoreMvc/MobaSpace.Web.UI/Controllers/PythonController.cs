using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MobaSpace.Web.UI.Controllers
{
    public class PythonController : Controller
    {
        //Return server status
        [Route("Server")]
        public void Index() {

            var httpRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5100/");

            httpRequest.ContentType = "charset=utf-8";

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();


            using(var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
            {
                HttpContext.Response.ContentType = "text/plain; charset=utf-8";
                HttpContext.Response.WriteAsync(streamReader.ReadToEnd());
            }

        }

        //Redirect request on 127.0.0.1:5100/api/checklist
        [Route("api/checklist")]
        [HttpGet]
        public async void ApiCheckList()
        {
            var url = "http://127.0.0.1:5100/api/checklist";

            var FormOldRequest = HttpContext.Request.Form;

            var EncodedForm = new FormUrlEncodedContent(FormOldRequest.Keys.ToDictionary(k => k, v => FormOldRequest[v].ToString()));

            var httpClient = new HttpClient();

            var Response = httpClient.PostAsync(url, EncodedForm).GetAwaiter().GetResult();

            await HttpContext.Response.WriteAsync(await Response.Content.ReadAsStringAsync());

        }

        //Redirect request on 127.0.0.1:5100/api/caregiverlist
        [Route("api/caregiverlist")]
        [HttpGet]
        public async void ApiCareGiverList()
        {
            var url = "http://127.0.0.1:5100/api/caregiverlist";

            var FormOldRequest = HttpContext.Request.Form;

            var EncodedForm = new FormUrlEncodedContent(FormOldRequest.Keys.ToDictionary(k => k, v => FormOldRequest[v].ToString()));

            var httpClient = new HttpClient();

            var Response = httpClient.PostAsync(url, EncodedForm).GetAwaiter().GetResult();

            await HttpContext.Response.WriteAsync(await Response.Content.ReadAsStringAsync());
        }

        //Redirect request on 127.0.0.1:5100/api/form
        [Route("api/form")]
        [HttpGet]
        public async void ApiForm()
        {
#if DEBUG
            var url = "https://smysyspad.mobaspace.com/api/form";
            HttpContext.Response.Headers.Add("Access - Control - Allow - Origin", " * ");
#elif !DEBUG
            var url = "http://127.0.0.1:5100/api/form";
#endif

            var FormOldRequest = HttpContext.Request.Form;

            var EncodedForm = new FormUrlEncodedContent(FormOldRequest.Keys.ToDictionary(k => k, v => FormOldRequest[v].ToString()));

            var httpClient = new HttpClient();

            var Response = httpClient.PostAsync(url, EncodedForm).GetAwaiter().GetResult();

            await HttpContext.Response.WriteAsync(await Response.Content.ReadAsStringAsync());
        }

        //Redirect request on 127.0.0.1:5100/withings_evt
        [Route("withings_evt")]
        [HttpPost]
        [HttpHead]
        public async void withings_event()
        {
            if (HttpContext.Request.Method == "HEAD")
            {

            }
            else
            {
#if !DEBUG
                var url = "http://127.0.0.1:5100/withings_evt";
#elif DEBUG
                var url = "https://smysyspad.mobaspace.com/withings_evt"; 
                HttpContext.Response.Headers.Add("Access - Control - Allow - Origin", " * ");
#endif
                var FormOldRequest = HttpContext.Request.Form;

                var EncodedForm = new FormUrlEncodedContent(FormOldRequest.Keys.ToDictionary(k => k, v => FormOldRequest[v].ToString()));

                var httpClient = new HttpClient();

                var Response = httpClient.PostAsync(url, EncodedForm).GetAwaiter().GetResult();

                await HttpContext.Response.WriteAsync(await Response.Content.ReadAsStringAsync());

            }
        }

        [Route("api/QCM")]
        [HttpGet]
        public async Task<string> GetQCM()
        {
#if DEBUG
            var url = "https://smysyspad.mobaspace.com/api/form";

            HttpContext.Response.Headers.Add("Access - Control - Allow - Origin", " * ");
#elif !DEBUG
            var url = "http://127.0.0.1:5100/api/form";
#endif
            var FormOldRequest = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
                                {
                                    {"appli", "777" },
                                    {"type", "syspad" }
                                });

            var EncodedForm = new FormUrlEncodedContent(FormOldRequest.Keys.ToDictionary(k => k, v => FormOldRequest[v].ToString()));

            var httpClient = new HttpClient();

            var Response = httpClient.PostAsync(url, EncodedForm).GetAwaiter().GetResult();

            var str = await Response.Content.ReadAsStringAsync();
            return str;
        }
    }
}
