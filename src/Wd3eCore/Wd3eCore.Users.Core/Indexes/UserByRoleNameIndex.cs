using System.Linq;
using Microsoft.AspNetCore.Identity;
using Wd3eCore.Users.Models;
using YesSql.Indexes;

namespace Wd3eCore.Users.Indexes
{
    public class UserByRoleNameIndex : ReduceIndex
    {
        public string RoleName { get; set; }
        public int Count { get; set; }
    }

    public class UserByRoleNameIndexProvider : IndexProvider<User>
    {
        private readonly ILookupNormalizer _keyNormalizer;

        public UserByRoleNameIndexProvider(ILookupNormalizer keyNormalizer)
        {
            _keyNormalizer = keyNormalizer;
        }

        public override void Describe(DescribeContext<User> context)
        {
            context.For<UserByRoleNameIndex, string>()
                .Map(user => user.RoleNames.Select(x => new UserByRoleNameIndex
                {
                    RoleName = NormalizeKey(x),
                    Count = 1
                }))
                .Group(user => user.RoleName)
                .Reduce(group => new UserByRoleNameIndex
                {
                    RoleName = group.Key,
                    Count = group.Sum(x => x.Count)
                })
                .Delete((index, map) =>
                {
                    index.Count -= map.Sum(x => x.Count);
                    return index.Count > 0 ? index : null;
                });
        }

        private string NormalizeKey(string key)
        {
            return _keyNormalizer == null ? key : _keyNormalizer.NormalizeName(key);
        }
    }
}
