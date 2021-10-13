using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDiscordBot.Bot.Modules
{
    public class General: ModuleBase
    {
        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("bip bop !@#$%^&*(");
        }

        [Command("info")]
        public async Task Info(SocketGuildUser targetUser = null)
        {
            var user = targetUser ?? Context.User;
            string description = string.Empty;

            if (user.Discriminator != 4194.ToString())
            {
                description = "In this message you can see some information about yourself!";
            }
            else
                description = "Thx god";

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithDescription(description)
                .AddField("Created at", user.CreatedAt.UtcDateTime)
                .AddField("Joined at", (user as SocketGuildUser).JoinedAt.Value.UtcDateTime)
                .WithColor(new Color(249, 179, 231))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
        }

        [Command("purification")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Purification(int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();

            try
            {
                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
                var result = await Context.Channel.SendMessageAsync($"{amount} messages deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Context.Channel.SendMessageAsync("Kurama has no permissions to manage chat");
            }
        }

        [Command("server")]
        public async Task ServerInfo()
        {

            var qwe = (Context.Guild as SocketGuild).Users.ToList();

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .AddField("Online users", (Context.Guild as SocketGuild).Users.Where(u => u.Status != UserStatus.Offline).Count())
                .AddField("Offline users", (Context.Guild as SocketGuild).Users.Where(u => u.Status == UserStatus.Offline).Count(), true)
                .AddField("Users in invisibility", (Context.Guild as SocketGuild).Users.Where(u => u.Status != UserStatus.Invisible).Count())
                .AddField("Member count", (Context.Guild as SocketGuild).MemberCount)
                .WithColor(new Color(249, 179, 231))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}
