using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TLPokemon.Api.Models
{
    public abstract class PokemonBase
    {
        // TODO decorate with json property name
        public PokemonBase(string name, string description, string habitat, bool isLegendary)
        {
            Name = name;
            OriginalDescription = description;
            Habitat = habitat;
            IsLegendary = isLegendary;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        protected string OriginalDescription { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Habitat { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLegendary { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string Description { get; }
    }
}
