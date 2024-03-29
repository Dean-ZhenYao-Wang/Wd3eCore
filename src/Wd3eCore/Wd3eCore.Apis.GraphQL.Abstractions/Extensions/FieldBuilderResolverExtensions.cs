using System;
using System.Threading.Tasks;
using GraphQL.Builders;
using GraphQL.Types;
using Wd3eCore.Apis.GraphQL.Resolvers;

namespace Wd3eCore.Apis.GraphQL
{
    public static class FieldBuilderResolverExtensions
    {
        public static FieldBuilder<TSourceType, TReturnType> ResolveLockedAsync<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> builder, Func<ResolveFieldContext<TSourceType>, Task<TReturnType>> resolve)
        {
            return builder.Resolve(new LockedAsyncFieldResolver<TSourceType, TReturnType>(resolve));
        }
    }
}
