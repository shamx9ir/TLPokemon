using Autofac;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Models;
using TLPokemon.Api.Services.Network;

namespace TLPokemon.Api.Services.Pokemon
{
    public class PokemonService : IPokemonService
    {
        private readonly ILifetimeScope lifetimeScope;
        private readonly INetworkService networkService;
        private readonly string apiEndpoint;
        private readonly string descriptionLanguage;

        public PokemonService(ILifetimeScope lifetimeScope, INetworkService networkService, IOptions<TLPokemonConfiguration> config)
        {
            this.networkService = networkService;
            this.lifetimeScope = lifetimeScope;
            apiEndpoint = config.Value.PokemonServiceEndpoint;
            descriptionLanguage = config.Value.DescriptionLanguage;
        }

        public PokemonBase Get<T>(string name) where T : PokemonBase
        {
            var url = $"{apiEndpoint}{name}";
            var jsonString = networkService.GetJsonString(url).Result;

            var data = JsonConvert.DeserializeObject<dynamic>(jsonString);
            var pokemonName = Convert.ToString(data.name);

            var flavorList = data.flavor_text_entries as IEnumerable<dynamic>;
            var description = Convert.ToString(flavorList.Where(flavor => Convert.ToString(flavor.language.name) == descriptionLanguage).FirstOrDefault()?.flavor_text);
            
            var habitat = Convert.ToString(data.habitat.name);
            var isLegendary = Convert.ToBoolean(data.is_legendary);

            return lifetimeScope.Resolve<T>(
                new NamedParameter("name", pokemonName),
                new NamedParameter("description", description),
                new NamedParameter("habitat", habitat),
                new NamedParameter("isLegendary", isLegendary));
        }
    }
}
