using System.Threading.Tasks;

namespace Wd3eCore.Environment.Commands
{
    public interface ICommandHandler
    {
        Task ExecuteAsync(CommandContext context);
    }
}
