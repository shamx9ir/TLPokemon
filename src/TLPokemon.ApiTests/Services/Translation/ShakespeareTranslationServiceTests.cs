using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Services.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using TLPokemon.Api.Services.Network;
using Moq;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TLPokemon.Api.Models;
using Autofac;
using System.Text.Json;

namespace TLPokemon.Api.Services.Translation.Tests
{
    /// <summary>
    /// When class is instantiated api endpoint is read from config.
    /// </summary>
    [TestClass()]
    public class ShakespeareTranslationServiceTests
    {
        /// <summary>
        /// ShakespearTranslationService uses api endpoint from configuration and text parameter
        /// </summary>
        [TestMethod()]
        public void ShakespearTranslationServiceTest()
        {
            var shakespeareEndpoint = "https://api.funtranslations.com/translate/shakespeare.json";
            var networkServiceMock = new Mock<INetworkService>();
            var optionsMock = new Mock<IOptions<TLPokemonConfiguration>>();
            var config = new TLPokemonConfiguration();
            config.ShakespeareTranslationServiceEndpoint = shakespeareEndpoint;
            optionsMock.Setup(m => m.Value).Returns(config);


            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(optionsMock.Object);
                builder.RegisterInstance(networkServiceMock.Object);
            }))
            {
                var shakespearTranslationService = mock.Create<ShakespeareTranslationService>();
                var result = shakespearTranslationService.Translate("test");

                networkServiceMock.Verify(m => m.PostJsonString(
                    It.Is<string>(arg => arg == shakespeareEndpoint),
                    It.Is<StringContent>(arg => arg.ReadAsStringAsync().Result == "{\"text\":\"test\"}")));
            }
        }
    }
}