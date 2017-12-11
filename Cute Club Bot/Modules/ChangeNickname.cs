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
            ulong ellaID = 109757292970262528;
            bool cor = false;
            if (O == Context.Message.Author.Id || C.Id == Context.Message.Author.Id || ellaID == Context.Message.Author.Id)
                cor = true;
            else
                return;

            BotSettings temp = new BotSettings();
            temp.settings.Nickname = pNickname;
            temp.Serialize();

            
            var guilds = await Context.Client.GetGuildsAsync();
            foreach (var g in guilds)
            {
                var user = await g.GetUserAsync(Context.Client.CurrentUser.Id);
                await user.ModifyAsync(x =>
                {
                    x.Nickname = temp.settings.Nickname;
                });
            }

            await ReplyAsync($"Nickname has been changed to {pNickname} successfully.");
            await new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author.Mention} changed bot nickname to {pNickname}.", Context);
        }
    }
}