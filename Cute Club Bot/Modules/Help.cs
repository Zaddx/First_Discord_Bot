using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Discord.WebSocket;

namespace Cute_Club_Bot.Modules
{
    [Group("Help")]
    public class Help : ModuleBase
    {
        [Command]
        [Remarks("Shows all commands that the bot haves")]
        public async Task HelpAsync()
        {
            List<string> prefixes;
            var user = Context.Message.Attachments as SocketGuildUser;

            var dmChannel = await Context.Message.Author.GetOrCreateDMChannelAsync();
        }

        [Command]
        [Remarks("Shows the specific details for a function")]
    }
}
