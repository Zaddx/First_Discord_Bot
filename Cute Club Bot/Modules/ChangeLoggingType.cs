using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Cute_Club_Bot.Logging;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Modules
{
    public class ChangeLoggingType : ModuleBase
    {
        [Command("LogType")]
        [Remarks("The LogType must either be File or Channel. Ex. !LogType Channel")]
        public async Task ChangeLogType(string logType)
        {
            if (logType.ToLower() != "file" && logType.ToLower() != "channel")
            {
                await ReplyAsync($"This command only works with types: File or Channel.");
                return;
            }

            LoggingConfiguration logging = new LoggingConfiguration();
            logging.config.LogType = logType;
            logging.Serialize();

            await ReplyAsync($"Logging type has been changed to {logType} successfully.");
            await new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author.Mention} changed logging type to {logType}.", Context);
        }
    }
}
