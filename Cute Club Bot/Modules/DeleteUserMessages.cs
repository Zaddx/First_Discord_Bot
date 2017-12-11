﻿using Discord;
using System.IO;
using System.Net.Http;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Cute_Club_Bot.Logging;
using System.Threading.Tasks;

namespace Cute_Club_Bot.Modules
{
    [Group("DeleteUserMessages")]
    public class DeleteUserMessages : ModuleBase
    {
        [Command]
        [Remarks("Deletes messages from a specific user, in the channel the command was used in, or a channel specified.")]
        [RequireBotPermission(GuildPermission.ManageMessages), RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task DeleteMessages(IGuildUser user, IMessageChannel channel = null)
        {
            System.Collections.Generic.IEnumerable<IMessage> msgs = null;
            if (channel == null)
            {
                channel = Context.Channel;
                msgs = await Context.Channel.GetMessagesAsync().Flatten();
            }
            else
            {
                var c = await Context.Guild.GetTextChannelAsync(channel.Id);
                msgs = await c.GetMessagesAsync().Flatten();
            }

            foreach (var msg in msgs)
                if (msg.Author == user)
                    await msg.DeleteAsync();

            await new ChangeLog().LogChange($"[{Context.Message.Timestamp}]: {Context.Message.Author.Mention} deleted messages from {user.Nickname} in {MentionUtils.MentionChannel(channel.Id)}.", Context);
        }
    }
}
