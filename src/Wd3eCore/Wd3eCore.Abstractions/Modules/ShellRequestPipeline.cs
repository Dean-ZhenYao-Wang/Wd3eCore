using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wd3eCore.Environment.Shell.Builders;

namespace Wd3eCore.Modules
{
    public class ShellRequestPipeline : IShellPipeline
    {
        public RequestDelegate Next { get; set; }
        public Task Invoke(object context) => Next(context as HttpContext);
    }
}
