using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;

namespace TLPokemon.Api.Models.Tests
{
    [TestClass()]
    public class PokemonTests
    {
        /// <summary>
        /// Checks that model constructor assigns values correctly
        /// </summary>
        [TestMethod()]
        public void PokemonTest()
        {
            var pokemon = new Pokemon("A", "B", "C", true);

            Assert.AreEqual("A", pokemon.Name);
            Assert.AreEqual("B", pokemon.Description);
            Assert.AreEqual("C", pokemon.Habitat);
            Assert.AreEqual(true, pokemon.isLegendary);
        }
    }
}