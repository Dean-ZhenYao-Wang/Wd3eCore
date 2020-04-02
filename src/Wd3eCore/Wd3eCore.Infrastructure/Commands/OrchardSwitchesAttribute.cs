using System;
using System.Collections.Generic;
using System.Linq;

namespace Wd3eCore.Environment.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Wd3eSwitchesAttribute : Attribute
    {
        private readonly string _switches;

        public Wd3eSwitchesAttribute(string switches)
        {
            _switches = switches;
        }

        public IEnumerable<string> Switches
        {
            get
            {
                return (_switches ?? "").Trim().Split(',').Select(s => s.Trim());
            }
        }
    }
}
