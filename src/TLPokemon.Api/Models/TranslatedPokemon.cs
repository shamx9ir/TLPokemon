using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Services.Translation;

namespace TLPokemon.Api.Models
{
    public class TranslatedPokemon : PokemonBase
    {
        private readonly ITranslationService translationService;

        public TranslatedPokemon(string name, string description, string habitat, bool isLegendary, ILifetimeScope lifetimeScope)
            : base(name, description, habitat, isLegendary)
        {
            if (isYodaTranslation)
                translationService = lifetimeScope.Resolve<YodaTranslationService>();
            else
                translationService = lifetimeScope.Resolve<ShakespeareTranslationService>();
        }


        public override string Description => translationService.Translate(OriginalDescription);

        private bool isYodaTranslation => Habitat == "cave" || IsLegendary;
    }
}
