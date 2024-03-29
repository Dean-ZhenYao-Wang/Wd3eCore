using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Builders;
using GraphQL.Types;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Apis.GraphQL
{
    public static class PermissionsExtensions
    {
        private static readonly string MetaDataKey = "Permissions";

        public static void RequirePermission(this IProvideMetadata type, Permission permission, object resource = null)
        {
            var permissions = type.GetMetadata<List<GraphQLPermissionContext>>(MetaDataKey);

            if (permissions == null)
            {
                type.Metadata[MetaDataKey] = permissions = new List<GraphQLPermissionContext>();
            }

            permissions.Add(new GraphQLPermissionContext(permission, resource));
        }

        public static FieldBuilder<TSourceType, TReturnType> RequirePermission<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> builder, Permission permission, object resource = null)
        {
            builder.FieldType.RequirePermission(permission, resource);
            return builder;
        }

        public static IEnumerable<GraphQLPermissionContext> GetPermissions(this IProvideMetadata type)
        {
            return type?.GetMetadata<List<GraphQLPermissionContext>>(MetaDataKey) ?? Enumerable.Empty<GraphQLPermissionContext>();
        }

        public static bool HasPermissions(this IProvideMetadata type)
        {
            return type != null && type.HasMetadata(MetaDataKey);
        }
    }
}
