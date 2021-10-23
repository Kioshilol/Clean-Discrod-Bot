using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartDiscordBot.Services
{
    public class BaseApiController
    {
        private HttpClient _httpClient;

        public BaseApiController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetRequest(string url)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            string responseBody = null;

            try
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            catch { }

            return responseBody;
        }
    }
}
