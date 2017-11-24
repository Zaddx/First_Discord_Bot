using System.Threading.Tasks;
using Discord.Commands;
using System.Net.Http;
using System.Net;
using SixLabors.ImageSharp;
using System.IO;
using Newtonsoft.Json.Linq;
using Discord;
using System;

namespace Cute_Club_Bot.Modules
{
    public class ChangeAvatar : ModuleBase
    {
        [Command("Avatar")]
        public async Task Avatar ()
        {
            var attachments = Context.Message.Attachments;
            foreach (IAttachment attachment in attachments)
            {
                if (attachment.Filename.Contains("png") || attachment.Filename.Contains("jpg"))
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(attachment.Url);
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    await Context.Client.CurrentUser.ModifyAsync(u =>
                    {
                        u.Avatar = new Discord.Image(stream);
                    });
                }
            }
        }
    }
}
