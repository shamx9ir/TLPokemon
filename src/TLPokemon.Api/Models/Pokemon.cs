using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TLPokemon.Api.Models
{
    public class Pokemon : PokemonBase
    {
        public Pokemon(string name, string description, string habitat, bool isLegendary)
            : base(name, description, habitat, isLegendary)
        {

        }

        public override string Description => OriginalDescription;
    }
}
