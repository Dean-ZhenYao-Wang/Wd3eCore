using Newtonsoft.Json.Linq;

namespace Wd3eCore.Entities
{
    public interface IEntity
    {
        JObject Properties { get; }
    }
}
