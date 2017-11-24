using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Cute_Club_Bot.Modules
{
    [Group("InstantInvite")]
    public class CreateInstantInvite : ModuleBase
    {
        [Command]
        public async Task InstantInvite()
        {
            var g = Context.Guild;
            var cs = await Context.Guild.GetChannelsAsync();
            IGuildChannel c = null;
            foreach (var channel in cs)
            {
                if (channel.Name == "general")
                    c = channel;
            }
            var i = await c.CreateInviteAsync(1800, 0, false, true);
            await ReplyAsync(i.Url);
        }
    }
}
