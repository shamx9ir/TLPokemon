using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Services.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TLPokemon.Api.Services.Network;
using Microsoft.Extensions.Options;
using TLPokemon.Api.Models;
using Autofac.Extras.Moq;
using Autofac;
using System.Net.Http;

namespace TLPokemon.Api.Services.Translation.Tests
{
    /// <summary>
    /// YodaTranslationService uses api endpoint from configuration and text parameter
    /// </summary>
    [TestClass()]
    public class YodaTranslationServiceTests
    {
        [TestMethod()]
        public void YodaTranslationServiceTest()
        {
            var yodaEndpoint = "https://api.funtranslations.com/translate/yoda.json";
            var networkServiceMock = new Mock<INetworkService>();
            var optionsMock = new Mock<IOptions<TLPokemonConfiguration>>();
            var config = new TLPokemonConfiguration();
            config.YodaTranslationServiceEndpoint = yodaEndpoint;
            optionsMock.Setup(m => m.Value).Returns(config);

            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(optionsMock.Object);
                builder.RegisterInstance(networkServiceMock.Object);
            }))
            {
                var shakespearTranslationService = mock.Create<YodaTranslationService>();
                var result = shakespearTranslationService.Translate("test");

                networkServiceMock.Verify(m => m.PostJsonString(
                    It.Is<string>(arg => arg == yodaEndpoint),
                    It.Is<StringContent>(arg => arg.ReadAsStringAsync().Result == "{\"text\":\"test\"}")));

            }
        }
    }
}