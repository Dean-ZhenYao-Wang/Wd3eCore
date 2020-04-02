using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Wd3eCore.Workflows.Abstractions.Converters;

namespace Wd3eCore.Workflows.Abstractions.Models
{
    public class Outcome
    {
        public Outcome(LocalizedString displayName) : this(displayName.Name, displayName)
        {
        }

        public Outcome(string name, LocalizedString displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public string Name { get; }

        [JsonConverter(typeof(LocalizedStringConverter))]
        public LocalizedString DisplayName { get; }
    }
}
