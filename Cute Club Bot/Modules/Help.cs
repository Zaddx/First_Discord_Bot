using System.Linq;
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
        CommandService _service;

        public Help(CommandService service)
        {
            _service = service;
        }

        [Command]
        [Remarks("Shows all commands that the bot haves")]
        public async Task HelpAsync()
        {
            string prefix = "";
            var user = Context.Message.Attachments as SocketGuildUser;

            var dmChannel = await Context.Message.Author.GetOrCreateDMChannelAsync();
            BotConfiguration botConfig = new BotConfiguration();

            int argPos = 0;
            if (Context.Message.HasStringPrefix(botConfig.config.Owner_Prefix, ref argPos))
                prefix = botConfig.config.Owner_Prefix;
            if (Context.Message.HasStringPrefix(botConfig.config.Admin_Prefix, ref argPos))
                prefix = botConfig.config.Admin_Prefix;
            if (Context.Message.HasStringPrefix(botConfig.config.Mod_Prefix, ref argPos))
                prefix = botConfig.config.Mod_Prefix;
            if (Context.Message.HasStringPrefix(botConfig.config.Everyone_Prefix, ref argPos))
                prefix = botConfig.config.Everyone_Prefix;

            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;

                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);

                    if (result.IsSuccess)
                    {
                        if (cmd.Name.Contains("Help")) continue;
                        description += $"{prefix}{cmd.Name}\n";
                    }
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }
            await dmChannel.SendMessageAsync("", false, builder.Build());
        }

        [Command]
        [Remarks("Shows the specific details for a function")]
        public async Task HelpAsync(string command)
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                return;
            }

            string prefix = "";

            BotConfiguration botConfig = new BotConfiguration();

            int argPos = 0;
            if (Context.Message.HasStringPrefix(botConfig.config.Owner_Prefix, ref argPos))
                prefix = botConfig.config.Owner_Prefix;
            if (Context.Message.HasStringPrefix(botConfig.config.Admin_Prefix, ref argPos))
                prefix = botConfig.config.Admin_Prefix;
            if (Context.Message.HasStringPrefix(botConfig.config.Mod_Prefix, ref argPos))
                prefix = botConfig.config.Mod_Prefix;
            if (Context.Message.HasStringPrefix(botConfig.config.Everyone_Prefix, ref argPos))
                prefix = botConfig.config.Everyone_Prefix;

            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = $"Here are some commands like **{command}**"
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Name);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" +
                              $"Remarks: {cmd.Remarks}";
                    x.IsInline = false;
                });
            }
            await dmChannel.SendMessageAsync("", false, builder.Build());
        }
    }
}
