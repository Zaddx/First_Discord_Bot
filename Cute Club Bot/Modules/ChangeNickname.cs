using System.Threading.Tasks;
using Discord.Commands;
using Cute_Club_Bot.Jsons;

namespace Cute_Club_Bot.Modules
{
    public class ChangeNickname : ModuleBase
    {
        [Command("Nickname"), RequireOwner]
        public async Task Nickname(string pNickname)
        {
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id) return;

            BotSettings temp = new BotSettings();
            temp.botSettings.Nickname = pNickname;
            temp.Serialize();

            var guild = await Context.Client.GetGuildAsync(342379038243028992);
            var user = await guild.GetUserAsync(Context.Client.CurrentUser.Id);
            await user.ModifyAsync(x =>
            {
                x.Nickname = temp.botSettings.Nickname;
            });
        }
    }
}