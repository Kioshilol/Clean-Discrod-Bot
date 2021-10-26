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

        public CommandHandler(IServiceProvider provider, IConfiguration configuration, ILogger<CommandHandler> logger, DiscordSocketClient client, CommandService commandService) : base(client, logger)
        {
            _provider = provider;
            _configuration = configuration;
            _commandService = commandService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Client.MessageReceived += OnMessageReceived;
            Client.JoinedGuild += OnJoinedGuild;
            Client.ReactionAdded += OnReactionAdded;
            _commandService.CommandExecuted += OnCommandExecute;

            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnReactionAdded(Discord.Cacheable<Discord.IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if(arg3.Message.IsSpecified)
            {
                if (arg3.Message.Value.Author.Id == Client.CurrentUser.Id)
                {
                    await arg3.Channel.SendMessageAsync(Constants.ThxForReaction);
                }
            }
            
        }

        private async Task OnJoinedGuild(SocketGuild arg)
        {
            await arg.DefaultChannel.SendMessageAsync(Constants.Greetings);
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage message))
                return;

            if (message.Source != Discord.MessageSource.User)
                return;

            var argsPos = 0;

            if (!message.HasStringPrefix(_configuration["prefix"], ref argsPos) && !message.HasMentionPrefix(Client.CurrentUser, ref argsPos))
                return;

            var context = new SocketCommandContext(Client, message);
            await _commandService.ExecuteAsync(context, argsPos, _provider);
        }

        private async Task OnCommandExecute(Discord.Optional<CommandInfo> arg1, ICommandContext arg2, IResult arg3)
        {
            if (!arg1.IsSpecified || !arg3.IsSuccess)
                await arg2.Channel.SendMessageAsync($"Error: {arg3}");
        }
    }
}
