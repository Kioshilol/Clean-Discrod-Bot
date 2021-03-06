using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartDiscordBot.Bot.Services
{
    public class StartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfiguration _configuration;

        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfiguration configuration)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _configuration = configuration;
        }

        public async Task StartAsync()
        {
            string token = _configuration["tokens:discord"];

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("_________________Please provide your DiscordToken in config file!_________________");
                return;
            }

            await _discord.LoginAsync(Discord.TokenType.Bot, token);
            await _discord.StartAsync();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}
