using Autofac;
using Autofac.Core;
using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLPokemon.ApiTests.Helpers
{
    public static class MockHelper
    {
        public static TypedParameter GetTypedParameter(this object obj)
        {
            return new TypedParameter(obj.GetType(), obj);
        }

        public static T Create<T>(this AutoMock mock, params object[] parameters)
        {
            var paramList = new List<Parameter>();
            foreach(var p in parameters)
            {
                paramList.Add(p.GetTypedParameter());
            }
            return mock.Create<T>(paramList.ToArray());
        }
    }
}
