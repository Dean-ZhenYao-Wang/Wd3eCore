using System.Security.Claims;

namespace Wd3eCore.Security
{
    /// <summary>
    /// 表示授予角色内所有用户的claim
    /// </summary>
    public class RoleClaim
    {
        /// <summary>
        /// 获取或设置此claim的claim类型。
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 获取或设置此claim的claim值。
        /// </summary>
        public string ClaimValue { get; set; }

        public Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }
    }
}
