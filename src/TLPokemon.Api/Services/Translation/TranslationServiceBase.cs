using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TLPokemon.Api.Services.Network;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TLPokemon.Api.Services.Translation
{
    public class TranslationServiceBase : ITranslationService
    {
        private readonly string apiEndpoint;
        private readonly INetworkService networkService;

        public TranslationServiceBase(string apiEndpoint, INetworkService networkService)
        {
            this.apiEndpoint = apiEndpoint;
            this.networkService = networkService;
        }

        public string Translate(string text)
        {
            try
            {
                var jsonRequestContent = new { text = text };
                var stringContent = new StringContent(JsonSerializer.Serialize(jsonRequestContent), Encoding.Default, "application/json");
                var jsonString = networkService.PostJsonString(apiEndpoint, stringContent).Result;
                var data = JsonConvert.DeserializeObject<dynamic>(jsonString);
                var translatedText = Convert.ToString(data?.contents?.translated);

                return translatedText ?? text;
            }
            catch (Exception ex)
            {
                // TODO log
                return text;
            }
        }
    }
}
