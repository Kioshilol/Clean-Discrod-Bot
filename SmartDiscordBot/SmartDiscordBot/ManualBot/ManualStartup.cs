using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using SmartDiscordBot.Bot.Services;
using SmartDiscordBot.Helpers;
using SmartDiscordBot.Interfaces;
using SmartDiscordBot.Services;
using SmartDiscordBot.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartDiscordBot.ManualBot
{
    public class ManualStartup : IStartup
    {
        public static IConfiguration Configuration { get; private set; }

        private readonly IServiceProvider _serviceProvider;

        public ManualStartup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync()
        {
            _serviceProvider.GetRequiredService<CommandHandler>();
            await _serviceProvider.GetRequiredService<StartupService>().StartAsync();
            await Task.Delay(-1);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            SetupConfigurtaion();

            services
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = Discord.LogSeverity.Verbose,
                    MessageCacheSize = 1000,
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = Discord.LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = false,
                }))
                .AddSingleton<CommandHandler>()
                .AddSingleton<StartupService>()
                .AddSingleton(Configuration)
                .AddSingleton<IApiController, ApiController>();
        }

        private static void SetupConfigurtaion()
        {
            var targetDirectoyry = DirectoryHelper.GetFileDirectory(Constants.ManualBotConfig);

            var builder = new ConfigurationBuilder()
               .SetBasePath(targetDirectoyry)
               .AddYamlFile(Constants.ManualBotConfig);

            try
            {
                Configuration = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
