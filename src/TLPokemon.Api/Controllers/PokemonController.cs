using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Models;
using TLPokemon.Api.Services.Pokemon;

namespace TLPokemon.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService pokemonService;
        public PokemonController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            return Ok(pokemonService.Get<Pokemon>(name));
        }

        [HttpGet("translated/{name}")]
        public IActionResult GetTranslated(string name)
        {
            return Ok(pokemonService.Get<TranslatedPokemon>(name));
        }
    }
}
