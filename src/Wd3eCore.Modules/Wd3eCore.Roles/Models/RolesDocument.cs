using System.Collections.Generic;
using Wd3eCore.Security;

namespace Wd3eCore.Roles.Models
{
    public class RolesDocument
    {
        public int Id { get; set; }
        public List<Role> Roles { get; } = new List<Role>();
        public int Serial { get; set; }
    }
}
