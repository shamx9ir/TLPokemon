using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TLPokemon.Api.Models
{
    public class TLPokemonConfiguration
    {
        public string PokemonServiceEndpoint { get; set; }

        public string YodaTranslationServiceEndpoint { get; set; }

        public string ShakespeareTranslationServiceEndpoint { get; set; }

        public string DescriptionLanguage { get; set; }
    }
}
