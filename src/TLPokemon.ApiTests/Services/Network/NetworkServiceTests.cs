using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Services.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Net.Http;
using Autofac.Extras.Moq;
using Autofac;
using Moq.Protected;
using System.Net;
using System.Threading;
using System.Net.Http.Json;

namespace TLPokemon.Api.Services.Network.Tests
{
    [TestClass()]
    public class NetworkServiceTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public async Task GetJsonStringTest()
        {
            var url = "https://truelayer.com/";
            var jsonString = "{ status: 'success' }";
            var jsonStringUnicoded = "\"{ status: \\u0027success\\u0027 }\"";

            var jsonContent = JsonContent.Create(jsonString);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = jsonContent });

            var client = new HttpClient(httpMessageHandlerMock.Object);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(client);

            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterInstance(httpClientFactoryMock.Object).As<IHttpClientFactory>();
            }))
            {
                var networkService = mock.Create<NetworkService>();
                var result = await networkService.GetJsonString(url);

                Assert.AreEqual(jsonStringUnicoded, result);
            }

        }
    }
}