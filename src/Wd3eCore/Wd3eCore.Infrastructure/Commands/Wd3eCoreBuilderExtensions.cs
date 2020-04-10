using Wd3eCore.Environment.Commands;
using Wd3eCore.Environment.Commands.Builtin;
using Wd3eCore.Environment.Commands.Parameters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// 添加主机级服务以提供CLI命令。
        /// </summary>
        public static Wd3eCoreBuilder AddCommands(this Wd3eCoreBuilder builder)
        {
            var services = builder.ApplicationServices;

            services.AddScoped<ICommandManager, DefaultCommandManager>();
            services.AddScoped<ICommandHandler, HelpCommand>();

            services.AddScoped<ICommandParametersParser, CommandParametersParser>();
            services.AddScoped<ICommandParser, CommandParser>();

            return builder;
        }
    }
}
