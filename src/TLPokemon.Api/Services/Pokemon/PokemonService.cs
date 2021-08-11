using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Models;

namespace TLPokemon.Api.Services.Pokemon
{
    public class PokemonService : IPokemonService
    {
        public PokemonBase Get<T>(string name) where T : PokemonBase
        {
            return new Models.Pokemon("pikachu", "buzzing mouse", "tube", false);
        }
    }
}
