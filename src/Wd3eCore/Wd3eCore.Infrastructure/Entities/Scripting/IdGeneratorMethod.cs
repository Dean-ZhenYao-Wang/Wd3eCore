using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Scripting;

namespace Wd3eCore.Entities.Scripting
{
    public class IdGeneratorMethod : IGlobalMethodProvider
    {
        private static GlobalMethod Uuid = new GlobalMethod
        {
            Name = "uuid",
            Method = serviceProvider => (Func<string>)(() =>
           {
               var idGenerator = serviceProvider.GetRequiredService<IIdGenerator>();
               return idGenerator.GenerateUniqueId();
           })
        };

        public IEnumerable<GlobalMethod> GetMethods()
        {
            yield return Uuid;
        }
    }
}
