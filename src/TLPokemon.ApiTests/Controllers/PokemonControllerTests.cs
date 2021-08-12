using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLPokemon.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Autofac;
using TLPokemon.Api.Services.Pokemon;
using Moq;
using TLPokemon.Api.Models;
using TLPokemon.ApiTests.Helpers;
using TLPokemon.Api.Services.Network;

namespace TLPokemon.Api.Controllers.Tests
{
    [TestClass()]
    public class PokemonControllerTests
    {
        [TestMethod()]
        public void Get_Calls_PokemonServiceGet_WithPokemonType()
        {
            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterType<PokemonController>();

            }))
            {
                var lifetimeScopeMock = new Mock<ILifetimeScope>();
                var networkServiceMock = new Mock<INetworkService>();

                var pokemonServiceMock = new Mock<PokemonService>(lifetimeScopeMock.Object, networkServiceMock.Object);
                pokemonServiceMock.Setup(m => m.Get<Pokemon>(It.IsAny<string>())).Returns(new Pokemon("mewtwo", "description", "habitat", true));

                var pokemonService = pokemonServiceMock.Object;
                // var controller = mock.Create<PokemonController>((IPokemonService)pokemonService);
                var controller = new PokemonController(pokemonService);
                var result = controller.Get("mewtwo");

                pokemonServiceMock.Verify(m => m.Get<Pokemon>(It.IsAny<string>()), Times.Once);

            }
        }

        [TestMethod()]
        public void Get_Calls_PokemonServiceGet_WithTranslatedPokemonType()
        {
            using (var mock = AutoMock.GetLoose(builder =>
            {
                builder.RegisterType<PokemonController>();

            }))
            {
                var lifetimeScopeMock = new Mock<ILifetimeScope>();
                var networkServiceMock = new Mock<INetworkService>();

                var pokemonServiceMock = new Mock<PokemonService>(lifetimeScopeMock.Object, networkServiceMock.Object);
                pokemonServiceMock.Setup(m => m.Get<TranslatedPokemon>(It.IsAny<string>())).Returns(new TranslatedPokemon("mewtwo", "description", "habitat", true));

                var pokemonService = pokemonServiceMock.Object;
                // var controller = mock.Create<PokemonController>((IPokemonService)pokemonService);
                var controller = new PokemonController(pokemonService);
                var result = controller.GetTranslated("mewtwo");

                pokemonServiceMock.Verify(m => m.Get<TranslatedPokemon>(It.IsAny<string>()), Times.Once);

            }
        }


    }
}