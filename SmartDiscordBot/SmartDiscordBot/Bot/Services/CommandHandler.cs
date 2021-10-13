using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDiscordBot.Bot.Services
{
    public class CommandHandler
    {
        public static IServiceProvider _provider;
        public static DiscordSocketClient _discord;
        public static CommandService _commands;
        public static IConfiguration _configuration;

        public CommandHandler(DiscordSocketClient discord, CommandService commands, IConfiguration configuration, IServiceProvider provider)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _configuration = configuration;

            _discord.Ready += OnReady;
            _discord.MessageReceived += OnMessageReceived;
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            if (msg.Author.IsBot)
                return;

            var context = new SocketCommandContext(_discord, msg);
            int prefixPosition = 0;

            if(msg.HasStringPrefix(_configuration["prefix"], ref prefixPosition) || msg.HasMentionPrefix(_discord.CurrentUser, ref prefixPosition))
            {
                var result = await _commands.ExecuteAsync(context, prefixPosition, _provider);

                if (!result.IsSuccess)
                {
                    var reason = result.Error;
                    await context.Channel.SendMessageAsync($"The following error occured: \n {reason}");
                    Console.WriteLine(reason);
                }
            }

        }

        private Task OnReady()
        {
            Console.WriteLine($"Connected as {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }
    }
}
