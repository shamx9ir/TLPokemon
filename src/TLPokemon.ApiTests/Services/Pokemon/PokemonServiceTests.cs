using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /// Calls Network service and parse data correctly
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

            /* TODO
            var lifetimeScopeMock = new Mock<ILifetimeScope>();
            var pokemonMock = new Mock<Models.Pokemon>();
            lifetimeScopeMock.Setup(m => m.Resolve<Models.Pokemon>(
                It.IsAny<NamedParameter>(),
                It.IsAny<NamedParameter>(),
                It.IsAny<NamedParameter>(),
                It.IsAny<NamedParameter>())
            ).Returns(pokemonMock.Object);
            */

            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(optionsMock.Object);
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();

                /* TODO
                builder.RegisterInstance(lifetimeScopeMock.Object).As<ILifetimeScope>();
                */
            }))
            {
                var pokemonService = mock.Create<PokemonService>();
                var result = pokemonService.Get<Models.Pokemon>("mewtwo");

                /* TODO 
                lifetimeScopeMock.Verify(m => m.Resolve<Models.Pokemon>(
                    It.Is<NamedParameter>(arg => arg.Value == new NamedParameter("name", "mewtwo").Value),
                    It.Is<NamedParameter>(arg => arg.Value == new NamedParameter("description", "It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.")),
                    It.Is<NamedParameter>(arg => arg.Value == new NamedParameter("habitat", "rare")),
                    It.Is<NamedParameter>(arg => arg.Value == new NamedParameter("isLegendary", true))
                ));
                */
                Assert.AreEqual(result.Name, "mewtwo");
                Assert.AreEqual(result.Description, "It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.");
                Assert.AreEqual(result.Habitat, "rare");
                Assert.AreEqual(result.isLegendary, true);

                networkServiceMock.Verify(m => m.GetJsonString(It.Is<string>(arg => arg == $"{pokemonServiceEndpoint}mewtwo")));


            }

        }
    }
}