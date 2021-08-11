using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Models;

namespace TLPokemon.Api.Services.Pokemon
{
    public interface IPokemonService
    {
        PokemonBase Get<T>(string name) where T : PokemonBase;
    }
}
