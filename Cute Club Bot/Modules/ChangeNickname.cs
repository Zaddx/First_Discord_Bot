using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Cute_Club_Bot.Logging;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Modules
{
    public class ChangeNickname : ModuleBase
    {
        [Command("Nickname")]
        [Remarks("This command takes in a name you would want to change the bot name to.")]
        public async Task Nickname(string pNickname)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            BotSettings temp = new BotSettings();
            temp.settings.Nickname = pNickname;
            temp.Serialize();

            var guild = await Context.Client.GetGuildAsync(342379038243028992);
            var user = await guild.GetUserAsync(Context.Client.CurrentUser.Id);
            await user.ModifyAsync(x =>
            {
                x.Nickname = temp.settings.Nickname;
            });

            await ReplyAsync($"Nickname has been changed to {pNickname} successfully.");
            await new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author} changed bot nickname to {pNickname}.", Context);
        }
    }
}