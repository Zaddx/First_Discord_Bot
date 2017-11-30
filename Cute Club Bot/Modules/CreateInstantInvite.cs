using Discord;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Modules
{
    [Group("InstantInvite")]
    public class CreateInstantInvite : ModuleBase
    {
        [Command]
        [Remarks("Creates a 30 minute, 1 use invite for the default channel. Which was set by the Owner of the server or the Owner of the me.")]
        public async Task InstantInvite()
        {
            BotConfiguration botConfig = new BotConfiguration();

            var g = Context.Guild;
            var cs = await Context.Guild.GetChannelsAsync();
            IGuildChannel c = null;
            foreach (var channel in cs)
            {
                if (channel.Name == botConfig.config.Invite_Channel)
                    c = channel;
            }
            var i = await c.CreateInviteAsync(1800, 1, false, true);
            await ReplyAsync(i.Url);
        }
    }
}
