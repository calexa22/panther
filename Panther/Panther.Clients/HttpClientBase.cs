using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Panther.Clients
{
    public class HttpClientBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        protected HttpClientBase(string baseUri, string apiToken)
        {
            BaseUri = new Uri(baseUri);
            _httpClient.BaseAddress = BaseUri;

            if (apiToken != null)
            {
                InitHeaders(apiToken);
            }
        }

        public Uri BaseUri { get; private set; }

        public async Task<T> GetAsync<T>(string requestUrl, Dictionary<string, string> query)
        {
            string url = GetFullUrlWithQuery(requestUrl, query);

            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException((int)response.StatusCode);
                }

                //var stringT = await response.Content.ReadAsStringAsync();
                using (Stream content = await response.Content.ReadAsStreamAsync())
                {
                    return DeserializeJsonFromStream<T>(content);
                }
            }
        }

        private void InitHeaders(string apiToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept
                       .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
        }

        private string GetFullUrlWithQuery(string requestUrl, Dictionary<string, string> query)
        {
            return QueryHelpers.AddQueryString(requestUrl, query);
        }

        private static T DeserializeJsonFromStream<T>(Stream content)
        {
            if (content == null || !content.CanRead)
            {
                return default(T);
            }

            using (StreamReader sReader = new StreamReader(content))
            using (JsonTextReader jtReader = new JsonTextReader(sReader))
            {
                T retVal = new JsonSerializer().Deserialize<T>(jtReader);
                return retVal;
            }
        }
    }
}
