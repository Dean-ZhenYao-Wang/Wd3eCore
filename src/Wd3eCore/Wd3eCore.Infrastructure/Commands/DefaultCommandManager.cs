using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Wd3eCore.Environment.Commands
{
    public class DefaultCommandManager : ICommandManager
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly CommandHandlerDescriptorBuilder _builder = new CommandHandlerDescriptorBuilder();
        private readonly IStringLocalizer S;

        public DefaultCommandManager(IEnumerable<ICommandHandler> commandHandlers,
            IStringLocalizer<DefaultCommandManager> localizer)
        {
            _commandHandlers = commandHandlers;

            S = localizer;
        }

        public async Task ExecuteAsync(CommandParameters parameters)
        {
            var matches = MatchCommands(parameters) ?? Enumerable.Empty<Match>();

            if (matches.Count() == 1)
            {
                var match = matches.Single();
                await match.CommandHandler.ExecuteAsync(match.Context);
            }
            else
            {
                var commandMatch = string.Join(" ", parameters.Arguments.ToArray());
                var commandList = string.Join(",", GetCommandDescriptors().SelectMany(d => d.Names).ToArray());
                if (matches.Any())
                {
                    throw new Exception(S["Multiple commands found matching arguments \"{0}\". Commands available: {1}.",
                        commandMatch, commandList]);
                }
                throw new Exception(S["No command found matching arguments \"{0}\". Commands available: {1}.",
                    commandMatch, commandList]);
            }
        }

        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return _commandHandlers.SelectMany(x => _builder.Build(x.GetType()).Commands);
        }

        private IEnumerable<Match> MatchCommands(CommandParameters parameters)
        {
            // 命令与参数匹配。第一个参数是命令，其他参数是命令的参数。
            return _commandHandlers.SelectMany(h =>
                    MatchCommands(parameters, parameters.Arguments.Count(), _builder.Build(h.GetType()), h)).ToList();
        }

        private static IEnumerable<Match> MatchCommands(CommandParameters parameters, int argCount, CommandHandlerDescriptor descriptor, ICommandHandler handler)
        {
            foreach (var commandDescriptor in descriptor.Commands)
            {
                foreach (var name in commandDescriptor.Names)
                {
                    var names = name.Split(' ');
                    var namesCount = names.Count();
                    // 我们在这里检查命令可以接收到的参数数量，这些参数是针对命令提供的参数来识别正确的命令并避免匹配多个命令。
                    if (name == string.Join(" ", parameters.Arguments.Take(namesCount)) && commandDescriptor.MethodInfo.GetParameters().Length == argCount - namesCount)
                    {
                        names = parameters.Arguments.ToArray();
                    }

                    if (parameters.Arguments.Take(argCount).SequenceEqual(names, StringComparer.OrdinalIgnoreCase))
                    {
                        yield return new Match
                        {
                            Context = new CommandContext
                            {
                                Arguments = parameters.Arguments.Skip(name.Split(' ').Count()),
                                Command = string.Join(" ", names),
                                CommandDescriptor = commandDescriptor,
                                Input = parameters.Input,
                                Output = parameters.Output,
                                Switches = parameters.Switches,
                            },
                            CommandHandler = handler
                        };
                    }
                }
            }
        }

        private class Match
        {
            public CommandContext Context { get; set; }
            public ICommandHandler CommandHandler { get; set; }
        }
    }
}
