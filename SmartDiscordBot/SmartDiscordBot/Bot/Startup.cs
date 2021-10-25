using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartDiscordBot.Bot.Services;
using SmartDiscordBot.Helpers;
using SmartDiscordBot.Interfaces;
using SmartDiscordBot.Services;
using SmartDiscordBot.Services.Interfaces;
using System.Threading.Tasks;

namespace SmartDiscordBot.Bot
{
    public class Startup : IStartup
    {
        public async Task RunAsync()
        {
            var targetDirectory = DirectoryHelper.GetFileDirectory(Constants.BotConfig);

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    var configuration = new ConfigurationBuilder()
                    .SetBasePath(targetDirectory)
                    .AddJsonFile(Constants.BotConfig)
                    .Build();

                    c.AddConfiguration(configuration);
                })
                .ConfigureLogging(l =>
                {
                    l.AddConsole();
                    l.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureDiscordHost((context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Verbose,
                        AlwaysDownloadUsers = true,
                        MessageCacheSize = 200,
                    };

                    config.Token = context.Configuration["token"];
                })
                .UseCommandService((context, config) =>
                {
                    config = new CommandServiceConfig
                    {
                        CaseSensitiveCommands = false,
                        LogLevel = LogSeverity.Verbose
                    };
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<CommandHandler>();
                    services.AddSingleton<IApiController, ApiController>();
                })
                .UseConsoleLifetime();

            await host.RunConsoleAsync();
        }
    }
}
