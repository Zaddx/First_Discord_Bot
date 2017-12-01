using Discord;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Cute_Club_Bot.Logging;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Modules
{
    public class ChangeLoggingChannel : ModuleBase
    {
        [Command("LogChannel")]
        [Remarks("Takes in a channel that you would like logs to be posted to (only posts if the Log Type is set to Channel. Ex. !LogChannel #botlog.")]
        public async Task ChangeLogChannel(IGuildChannel channel)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            LoggingConfiguration logging = new LoggingConfiguration();
            logging.config.LogChannel = channel.Name;
            logging.Serialize();

            await ReplyAsync($"Logging Channel has been changed to {channel} successfully.");
            await new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author} changed logging channel to {channel}.", Context);
        }
    }
}
