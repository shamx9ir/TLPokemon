using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Models;
using TLPokemon.Api.Services.Network;

namespace TLPokemon.Api.Services.Translation
{
    public class YodaTranslationService : TranslationServiceBase
    {
        public YodaTranslationService(INetworkService networkService, IOptions<TLPokemonConfiguration> config)
            : base(config.Value.YodaTranslationServiceEndpoint, networkService)
        {

        }
    }
}
