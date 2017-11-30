﻿using System.Threading.Tasks;
using Discord.Commands;
using System.Net.Http;
using System.IO;
using Discord;
using Cute_Club_Bot.Jsons;

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
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

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
            var dm = await C.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync($"{Context.Message.Author} changed avatar to {url}.");
        }
    }
}
