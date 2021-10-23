using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using SmartDiscordBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartDiscordBot.Bot.Modules
{
    public class Joy : ModuleBase
    {
        private IApiController _apiController;
        public Joy(IApiController apiController)
        {
            Console.WriteLine("Joy Module added");
            _apiController = apiController;
        }

        [Command("memo")]
        public async Task Memo()
        {
            var response = await _apiController.GetRedditMemo();

            if (response.IsSucces)
            {
                var data = response.ResponseData;
                var builder = new EmbedBuilder()
                        .WithImageUrl(data.ImageUrl)
                        .WithUrl("https://reddit.com" + data.PostUrl)
                        .WithTitle(data.Title)
                        .WithColor(Colors.Pink);

                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
            }
        }
    }
}
