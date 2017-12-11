using Discord;
using System.IO;
using System.Net.Http;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Cute_Club_Bot.Logging;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Modules
{
    public class ChangeAvatar : ModuleBase
    {
        [Command("Avatar")]
        [Remarks("This command works with an attachment only.")]
        public async Task Avatar ()
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

            string url = "";
            var attachments = Context.Message.Attachments;
            foreach (IAttachment attachment in attachments)
            {
                if (attachment.Filename.Contains("png") || attachment.Filename.Contains("jpg"))
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(attachment.Url);
                    url = attachment.Url;
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    await Context.Client.CurrentUser.ModifyAsync(u =>
                    {
                        u.Avatar = new Discord.Image(stream);
                    });

                    BotSettings temp = new BotSettings();
                    temp.settings.Avatar = attachment.Url;
                    temp.Serialize();
                }
            }

            await ReplyAsync("Avatar changed successfully.");
            await new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author.Mention} changed avatar to {url}.", Context);
        }
    }
}
