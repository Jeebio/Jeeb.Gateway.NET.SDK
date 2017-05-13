using System.IO;
using System.Net;
using System.Threading.Tasks;
using Jeeb.Gateway.Exceptions;
using Jeeb.Gateway.Models;
using Newtonsoft.Json;

namespace Jeeb.Gateway
{
    public class RequestBuilder
    {
        private readonly string apiBaseUrl = "https://jeeb.io/api/";

        public async Task<TOut> MakeRequestAsync<TOut>(RequestBuilderOptions options, object data = null)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create($"{apiBaseUrl}{options.Url}");
            webRequest.Method = options.Method;
            webRequest.ContentType = "application/json";

            if (options.Method.ToUpper() == "POST" && data != null)
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(data);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

            var httpResponse = (HttpWebResponse) await webRequest.GetResponseAsync();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = streamReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                    {
                        var result = JsonConvert.DeserializeObject<JeebApiResult<TOut>>(json);
                        if (result.HasError && result.ErrorCode.HasValue)
                            throw new JeebApiException(result.ErrorCode.Value, result.ErrorMessage);
                        return result.Result;
                    }
                }

            throw new JeebApiException("Failed to make request");
        }

        public TOut MakeRequest<TOut>(RequestBuilderOptions options, object data = null)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create($"{apiBaseUrl}{options.Url}");
            webRequest.Method = options.Method;
            webRequest.ContentType = "application/json";

            if (options.Method.ToUpper() == "POST" && data != null)
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(data);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

            var httpResponse = (HttpWebResponse) webRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = streamReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                    {
                        var result = JsonConvert.DeserializeObject<JeebApiResult<TOut>>(json);
                        if (result.HasError && result.ErrorCode.HasValue)
                            throw new JeebApiException(result.ErrorCode.Value, result.ErrorMessage);
                        return result.Result;
                    }
                }

            throw new JeebApiException("Failed to make request");
        }

        public class RequestBuilderOptions
        {
            public string Method { set; get; }


            public string Url { set; get; }
        }
    }
}