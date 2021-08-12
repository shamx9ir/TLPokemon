using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TLPokemon.Api.Services.Translation
{
    public interface ITranslationService
    {
        string Translate(string text);
    }
}
