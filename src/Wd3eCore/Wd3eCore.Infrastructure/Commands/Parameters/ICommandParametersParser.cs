using System.Collections.Generic;

namespace Wd3eCore.Environment.Commands.Parameters
{
    public interface ICommandParametersParser
    {
        CommandParameters Parse(IEnumerable<string> args);
    }
}
