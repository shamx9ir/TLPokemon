﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Services.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Moq;
using TLPokemon.Api.Services.Network;
using Autofac.Extras.Moq;
using Microsoft.Extensions.Options;
using TLPokemon.Api.Models;

namespace TLPokemon.Api.Services.Pokemon.Tests
{
    [TestClass()]
    public class PokemonServiceTests
    {
        /// <summary>
        /// Returns Pokemon object from mocked json data
        /// </summary>
        [TestMethod()]
        public void Get_ReturnsPokemon()
        {
            var pokemonApiJson = @"{
              ""flavor_text_entries"": [
                {
                  ""flavor_text"": ""It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments."",
                  ""language"": {
                    ""name"": ""en"",
                    ""url"": ""https://pokeapi.co/api/v2/language/9/""
                  },
                  ""version"": {
                    ""name"": ""red"",
                    ""url"": ""https://pokeapi.co/api/v2/version/1/""
                  }
                },
              ],
              ""habitat"": {
                ""name"": ""rare"",
                ""url"": ""https://pokeapi.co/api/v2/pokemon-habitat/5/""
              },
              ""is_legendary"": true,
              ""name"": ""mewtwo"",
            }";

            var pokemonServiceEndpoint = "https://pokeapi.co/api/v2/pokemon-species/";
            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.GetJsonString(It.IsAny<string>())).ReturnsAsync(pokemonApiJson);
            var optionsMock = new Mock<IOptions<TLPokemonConfiguration>>();
            var config = new TLPokemonConfiguration();
            config.PokemonServiceEndpoint = pokemonServiceEndpoint;
            config.DescriptionLanguage = "en";
            optionsMock.Setup(m => m.Value).Returns(config);


            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(optionsMock.Object);
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
            }))
            {
                var pokemonService = mock.Create<PokemonService>();
                var result = pokemonService.Get<Models.Pokemon>("mewtwo");

                Assert.AreEqual(result.Name, "mewtwo");
                Assert.AreEqual(result.Description, "It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.");
                Assert.AreEqual(result.Habitat, "rare");
                Assert.AreEqual(result.isLegendary, true);

                networkServiceMock.Verify(m => m.GetJsonString(It.Is<string>(arg => arg == $"{pokemonServiceEndpoint}mewtwo")));

            }

        }
    }
}