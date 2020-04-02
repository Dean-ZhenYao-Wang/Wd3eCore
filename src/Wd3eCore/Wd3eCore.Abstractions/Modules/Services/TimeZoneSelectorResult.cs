using System;
using System.Threading.Tasks;

namespace Wd3eCore.Modules
{
    public class TimeZoneSelectorResult
    {
        public int Priority { get; set; }
        public Func<Task<string>> TimeZoneId { get; set; }
    }
}
