using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TLPokemon.Api.Services.Network
{
    public interface INetworkService
    {
        Task<string> GetJsonString(string url);

    }
}
