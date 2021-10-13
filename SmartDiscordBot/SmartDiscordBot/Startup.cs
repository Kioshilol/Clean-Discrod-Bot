using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartDiscordBot.Bot.Services;
using System;
using System.Threading.Tasks;

namespace SmartDiscordBot
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("_config.yml");

            try
            {
                Configuration = builder.Build();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<CommandHandler>();
            await provider.GetRequiredService<StartupService>().StartAsync();

            await Task.Delay(-1);
        }

        private  void ConfigureServices(IServiceCollection services)
        {
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
                .AddSingleton(Configuration);
        }
    }
}
