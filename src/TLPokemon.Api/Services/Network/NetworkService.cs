using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TLPokemon.Api.Services.Network
{
    public class NetworkService : INetworkService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public NetworkService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetJsonString(string url)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostJsonString(string url, HttpContent content)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
