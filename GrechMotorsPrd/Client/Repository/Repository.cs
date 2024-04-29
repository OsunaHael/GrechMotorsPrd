using GrechMotorsPrd.Shared.Models;
using System.Text;
using System.Text.Json;

namespace GrechMotorsPrd.Client.Repository
{
    public class Repository : IRepository
    {
        private readonly HttpClient httpClient;
        
        public Repository(HttpClient httpClient)
        {
               this.httpClient = httpClient;
        }

        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions()
            { 
                PropertyNameCaseInsensitive = true 
            };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var httpResponse = await httpClient.GetAsync(url);
            if(httpResponse.IsSuccessStatusCode)
            {
                var response = await DeserializeResponse<T>(httpResponse, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, error:false, httpResponse);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, error:true, httpResponse);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T sendObject)
        {
            var sendObjectJson = JsonSerializer.Serialize(sendObject);
            var sendContent = new StringContent(sendObjectJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, sendContent);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>?> Post<T, TResponse>(string url, T sendObject)
        {
            var sendObjectJson = JsonSerializer.Serialize(sendObject);
            var sendContent = new StringContent(sendObjectJson, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, sendContent);
            if(responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializeResponse<TResponse>(responseHttp, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(response, error:false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        private async Task<T>DeserializeResponse<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }
    }
}
