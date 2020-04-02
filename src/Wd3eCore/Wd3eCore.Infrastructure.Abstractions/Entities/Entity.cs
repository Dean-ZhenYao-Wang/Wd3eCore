using Newtonsoft.Json.Linq;

namespace Wd3eCore.Entities
{
    public class Entity : IEntity
    {
        public JObject Properties { get; set; } = new JObject();
    }
}
