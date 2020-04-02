using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Wd3eCore.Scripting;

namespace Wd3eCore.Recipes
{
    public class ParametersMethodProvider : IGlobalMethodProvider
    {
        private readonly GlobalMethod _globalMethod;

        public ParametersMethodProvider(object environment)
        {
            var environmentObject = JObject.FromObject(environment);

            _globalMethod = new GlobalMethod
            {
                Name = "parameters",
                Method = serviceprovider => (Func<string, object>)(name =>
               {
                   return environmentObject[name].Value<string>();
               })
            };
        }

        public IEnumerable<GlobalMethod> GetMethods()
        {
            yield return _globalMethod;
        }
    }
}
