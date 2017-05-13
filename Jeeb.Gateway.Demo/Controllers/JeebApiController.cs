using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Jeeb.Gateway.Demo.Models;
using Jeeb.Gateway.Models;
using Newtonsoft.Json;

namespace Jeeb.Gateway.Demo.Controllers
{
    [RoutePrefix("api/jeeb")]
    [AllowAnonymous]
    public class JeebApiController : ApiController
    {
        private readonly string gatewayBaseUrl = "GATEWAY BASE URL GOES HERE";

        private readonly string conversionBaseUrl = "CONVERSION BASE URL GOES HERE";

        private readonly string signature = "6dfcdd77d87f4634897098a389a6b165";



        [HttpGet]
        [Route("rialtobtc/")]
        public decimal RialToBtc([FromUri]string value)
        {
            return new JeebApi(signature).CurrencyConvert(new CurrencyConvertRequest()
            {
                BaseCurrency = "IRR",
                TargetCurrency = "BTC",
                Value = Convert.ToDecimal(value),
            });
        }


        [HttpPost]
        [Route("issue/")]
        public IssueResultModel IssueInvoice(IssueRequestModel request)
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(gatewayBaseUrl + "issue/" + signature);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            request.AllowReject = true;
            request.NotificationUrl = "http://localhost/";
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(request);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            if (httpResponse != null)
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = streamReader.ReadToEnd();
                    if (json != null)
                    {
                        var result = JsonConvert.DeserializeObject<JeebResult<IssueResultModel>>(json);
                        if (result.HasError)
                        {
                            Debug.WriteLine($"{result.ErrorCode}: {result.ErrorMessage}");
                        }
                        else
                            return result.Result;
                        
                    }
                }

            return null;
        }


        [HttpGet]
        [Route("status/{referenceNo}")]
        public JeebResult<StatusResultModel> GetStatus([FromUri]string referenceNo)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(gatewayBaseUrl + "status/" + signature + "/" + referenceNo);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            if (httpResponse != null)
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = streamReader.ReadToEnd();
                    if (json != null)
                    {
                        var result = JsonConvert.DeserializeObject<JeebResult<StatusResultModel>>(json);
                        if (result.HasError)
                        {
                            Debug.WriteLine($"{result.ErrorCode}: {result.ErrorMessage}");
                        }
                        else
                            return result;
                    }
                }

            return null;
        }

        [HttpPost]
        [Route("confirm/{referenceNo}")]
        public JeebResult<ConfirmationResultModel> Confirm([FromUri] string referenceNo)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(gatewayBaseUrl + "confirm/" + signature + "/" + referenceNo);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            if (httpResponse != null)
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = streamReader.ReadToEnd();
                    if (json != null)
                    {
                        var result = JsonConvert.DeserializeObject<JeebResult<ConfirmationResultModel>>(json);
                        if (result.HasError)
                        {
                            Debug.WriteLine($"{result.ErrorCode}: {result.ErrorMessage}");
                        }
                        else
                            return result;
                    }
                }

            return null;

        }


        [HttpPost]
        [Route("webhook")]
        public void WebHook(NotificationModel notif)
        {
            NotificationHub.BroadCastNotif(notif);
        }


    }
}