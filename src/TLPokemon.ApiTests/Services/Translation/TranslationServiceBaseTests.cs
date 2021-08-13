using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Services.Network;
using Moq;
using Autofac.Extras.Moq;
using Autofac;
using TLPokemon.ApiTests.Helpers;
using System.Net.Http;

namespace TLPokemon.Api.Services.Translation.Tests
{
    /// <summary>
    /// Translate returns translated text.
    /// </summary>
    [TestClass()]
    public class TranslationServiceBaseTests
    {
        [TestMethod()]
        public void Translate_ReturnsTranslatedText()
        {
            var expectedString = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";
            var pokemonApiJson = @"{
                  ""success"": {
                    ""total"": 1
                  },
                  ""contents"": {
                    ""translated"": """ + expectedString + @""",
                    ""text"": ""You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die."",
                    ""translation"": ""shakespeare""
                  }
                }";

            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.PostJsonString(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(pokemonApiJson);

            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
            }))
            {
                var inputText = "Input text";
                var translationService = mock.Create<TranslationServiceBase>("https://api.funtranslations.com/translate/shakespeare.json");
                var result = translationService.Translate(inputText);

                Assert.AreEqual(expectedString, result);
            }
        }

        /// <summary>
        /// Translate returns original text when there is no translation
        /// </summary>
        [TestMethod()]
        public void Translate_NoTranslation()
        {
            var pokemonApiJson = @"{
                  ""success"": {
                    ""total"": 0
                  },
                  ""contents"": {
                  }
                }";

            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.PostJsonString(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(pokemonApiJson);

            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
            }))
            {
                var inputText = "Input text";
                var translationService = mock.Create<TranslationServiceBase>("https://api.funtranslations.com/translate/shakespeare.json");
                var result = translationService.Translate(inputText);

                Assert.AreEqual(inputText, result);
            }
        }

        /// <summary>
        /// Translate returns original text when there is an exception
        /// </summary>
        [TestMethod()]
        public void Translate_Exception()
        {
            var networkServiceMock = new Mock<INetworkService>();
            networkServiceMock.Setup(m => m.PostJsonString(It.IsAny<string>(), It.IsAny<HttpContent>())).Throws(new System.Exception());

            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(networkServiceMock.Object).As<INetworkService>();
            }))
            {
                var inputText = "Input text";
                var translationService = mock.Create<TranslationServiceBase>("https://api.funtranslations.com/translate/shakespeare.json");
                var result = translationService.Translate(inputText);

                Assert.AreEqual(inputText, result);
            }
        }
    }
}