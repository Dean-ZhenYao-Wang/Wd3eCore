using System.Collections.Generic;

namespace Wd3eCore.Environment.Commands
{
    public class CommandHandlerDescriptor
    {
        public IEnumerable<CommandDescriptor> Commands { get; set; }
    }
}
