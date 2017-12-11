using System.IO;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Logging
{
    public class ChangeLog
    {
        public async Task LogChange(string message, ICommandContext context)
        {
            if (new LoggingConfiguration().config.LogType == "File")
            {
                StreamWriter file = File.AppendText("../../Logging/changelog.txt");
                file.Write($"{message}\n");
                file.Close();
            }
            else if (new LoggingConfiguration().config.LogType == "Channel")
            {
                var gs = await context.Client.GetGuildsAsync();

                foreach (var g in gs)
                {
                    var channels = await g.GetTextChannelsAsync();

                    foreach (var channel in channels)
                        if (channel.Name == new LoggingConfiguration().config.LogChannel)
                            await channel.SendMessageAsync($"{message}");
                }
            }
            else
            {
                var app = await context.Client.GetApplicationInfoAsync();
                var C = app.Owner;
                var dmChannel = await C.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync($"An error occured in the LogChange function.\n Message being sent: {message}");
            }
        }
    }
}
