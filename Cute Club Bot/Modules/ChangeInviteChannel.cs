using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Cute_Club_Bot.Logging;

namespace Cute_Club_Bot.Modules
{
    public class ChangeInviteChannel : ModuleBase
    {
        [Command("InviteChannel")]
        [Remarks("The command takes in channel. Ex !InviteChannel #welcome.")]
        public async Task IChannel(IGuildChannel channel)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            BotConfiguration botConfig = new BotConfiguration();
            botConfig.config.Invite_Channel = channel.Name;
            botConfig.Serialize();

            await ReplyAsync($"Invite Channel has been changed to {channel} successfully.");
            new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author} changed invite channel to {channel}.");
        }
    }
}
