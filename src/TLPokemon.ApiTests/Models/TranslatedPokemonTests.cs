using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using TLPokemon.ApiTests.Helpers;
using TLPokemon.Api.Services.Translation;
using Moq;
using Autofac;
using TLPokemon.Api.Services.Network;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Autofac.Core;

namespace TLPokemon.Api.Models.Tests
{
    [TestClass()]
    public class TranslatedPokemonTests
    {
        /// <summary>
        /// Checks that model constructor assigns values correctly
        /// Test Yoda translation
        /// </summary>
        [TestMethod()]
        public void TranslatedPokemon_Constructor_Yoda_Legendary()
        {
            var apiJson = @"{
                  ""success"": {
                    ""total"": 1
                  },
                  ""contents"": {
                    ""translated"": ""D""
                  }
                }";

            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.PostJsonString(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(apiJson);

            var optionsMock = new Mock<IOptions<TLPokemonConfiguration>>();
            var config = new TLPokemonConfiguration();
            config.YodaTranslationServiceEndpoint = "";
            optionsMock.Setup(m => m.Value).Returns(config);


            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterType<YodaTranslationService>();
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
                builder.RegisterInstance(optionsMock.Object);
            }))
            {
                var translatedPokemon = mock.Create<TranslatedPokemon>(new Parameter[] { 
                    new NamedParameter("name", "A"),
                    new NamedParameter("description", "B"),
                    new NamedParameter("habitat", "C"),
                    new NamedParameter("isLegendary", true)
                });

                Assert.AreEqual("A", translatedPokemon.Name);
                Assert.AreEqual("D", translatedPokemon.Description);
                Assert.AreEqual("C", translatedPokemon.Habitat);
                Assert.AreEqual(true, translatedPokemon.IsLegendary);
            }

        }

        /// <summary>
        /// Test Yoda translation
        /// </summary>
        [TestMethod()]
        public void TranslatedPokemon_Constructor_Yoda_Cave()
        {
            var apiJson = @"{
                ""success"": {
                ""total"": 1
                },
                ""contents"": {
                ""translated"": ""D""
                }
            }";

            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.PostJsonString(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(apiJson);

            var optionsMock = new Mock<IOptions<TLPokemonConfiguration>>();
            var config = new TLPokemonConfiguration();
            config.YodaTranslationServiceEndpoint = "";
            optionsMock.Setup(m => m.Value).Returns(config);


            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterType<YodaTranslationService>();
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
                builder.RegisterInstance(optionsMock.Object);
            }))
            {
                var translatedPokemon = mock.Create<TranslatedPokemon>(new Parameter[] {
                new NamedParameter("name", "A"),
                new NamedParameter("description", "B"),
                new NamedParameter("habitat", "cave"),
                new NamedParameter("isLegendary", false)
            });

                Assert.AreEqual("D", translatedPokemon.Description);
            }

        }

        /// <summary>
        /// Test Shakespeare translation
        /// </summary>
        [TestMethod()]
        public void TranslatedPokemon_Constructor_Shakespeare()
        {
            var apiJson = @"{
                ""success"": {
                ""total"": 1
                },
                ""contents"": {
                ""translated"": ""E""
                }
            }";

            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.PostJsonString(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(apiJson);

            var optionsMock = new Mock<IOptions<TLPokemonConfiguration>>();
            var config = new TLPokemonConfiguration();
            config.ShakespeareTranslationServiceEndpoint = "";
            optionsMock.Setup(m => m.Value).Returns(config);


            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterType<ShakespeareTranslationService>();
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
                builder.RegisterInstance(optionsMock.Object);
            }))
            {
                var translatedPokemon = mock.Create<TranslatedPokemon>(new Parameter[] {
                new NamedParameter("name", "A"),
                new NamedParameter("description", "B"),
                new NamedParameter("habitat", "C"),
                new NamedParameter("isLegendary", false)
            });

                Assert.AreEqual("E", translatedPokemon.Description);
            }
        }
    }
}