﻿using System.Threading.Tasks;
using Discord.Commands;
using System.Net.Http;
using System.IO;
using Discord;

namespace Cute_Club_Bot.Modules
{
    public class ChangeAvatar : ModuleBase
    {
        [Command("Avatar"), RequireOwner]
        public async Task Avatar ()
        {
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id) return;

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
