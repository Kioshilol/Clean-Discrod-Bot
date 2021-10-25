using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SmartDiscordBot.Bot.Services
{
    public class CommandHandler : DiscordClientService
    {
        public readonly IServiceProvider _provider;
        public readonly IConfiguration _configuration;
        public readonly CommandService _commandService;

        public CommandHandler(IServiceProvider provider, IConfiguration configuration, ILogger<CommandHandler> logger, DiscordSocketClient client, CommandService commands) : base(client, logger)
        {
            _provider = provider;
            _configuration = configuration;
            _commandService = commands;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Client.MessageReceived += OnMessageReceived;
            _commandService.CommandExecuted += OnCommandExecute;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private Task OnCommandExecute(Discord.Optional<CommandInfo> arg1, ICommandContext arg2, IResult arg3)
        {
            return Task.CompletedTask;
        }

        private Task OnMessageReceived(SocketMessage arg)
        {
            return Task.CompletedTask;
        }
    }
}
