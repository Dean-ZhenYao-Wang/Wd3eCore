using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Environment.Commands
{
    public interface ICommandManager
    {
        Task ExecuteAsync(CommandParameters parameters);
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}
