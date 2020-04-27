using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wk.Study.Common.Helper
{
    public static class HttpHelper
    {

        public static async Task<T> SendAsync<T>(HttpClient httpClient, HttpRequestMessage httpRequestMessage, object obj, Dictionary<string, string> header = null)
        {
            if (header != null)
            {
                foreach (var pair in header)
                {
                    httpRequestMessage.Headers.Add(pair.Key, pair.Value);
                }
            }
            if (httpRequestMessage.Method == HttpMethod.Get)
            {
                string requestUrl = httpRequestMessage.RequestUri.ToString();
                string sParam = "";
                if (obj != null)
                {
                    foreach (var property in obj.GetType().GetProperties())
                    {
                        if (property.CanRead)
                        {
                            if (sParam == "")
                            {
                                sParam = property.Name + "=" + property.GetValue(obj, null);
                            }
                            else
                            {
                                sParam += "&" + property.Name + "=" + property.GetValue(obj, null);
                            }
                        }
                    }
                }

                if (requestUrl.IndexOf("?") > -1)
                {
                    requestUrl += "&" + sParam;
                }
                else
                {
                    requestUrl += "?" + sParam;
                }
                httpRequestMessage.RequestUri = new Uri(requestUrl);
            }
            else
            {
                httpRequestMessage.Content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");
            }
            var httpContent = await httpClient.SendAsync(httpRequestMessage);
            var str = await httpContent.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }


        public static async Task SendAsync(HttpClient httpClient, HttpRequestMessage httpRequestMessage, object obj, Dictionary<string, string> header = null)
        {
            httpRequestMessage.Content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");
            if (header != null)
            {
                foreach (var pair in header)
                {
                    httpRequestMessage.Headers.Add(pair.Key, pair.Value);
                }
            }
            var httpContent = await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
