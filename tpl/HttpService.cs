using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        public HttpService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="_params"></param>
        /// <returns></returns>
        public async Task<string> PostStringAsync(string url, IEnumerable<KeyValuePair<string, string>> _params)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(_params)
            };

            request.Headers.Add("accept", "application/json");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new WebException(response.ReasonPhrase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new WebException(response.ReasonPhrase);
        }
    }
}
