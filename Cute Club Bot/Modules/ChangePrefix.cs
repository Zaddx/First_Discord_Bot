﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Cute_Club_Bot.Jsons;
using Discord.WebSocket;

namespace Cute_Club_Bot.Modules
{
    [Group("Prefix")]
    public class ChangePrefix : ModuleBase
    {
        [Command("Everyone")]
        [Remarks("Changes the Everyone Prefix.")]
        public async Task EveryonePrefix(string prefix)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            BotConfiguration botConfig = new BotConfiguration();
            botConfig.config.Everyone_Prefix = prefix;
            botConfig.Serialize();

            await ReplyAsync($"The everyone prefix has been changed to {prefix} successfully.");
            var dm = await C.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync($"{Context.Message.Author} changed everyone prefix to {prefix}.");
        }

        [Command("Mod")]
        [Remarks("Changes the Mod Prefix.")]
        public async Task ModPrefix(string prefix)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            BotConfiguration botConfig = new BotConfiguration();
            botConfig.config.Mod_Prefix = prefix;
            botConfig.Serialize();

            await ReplyAsync($"The mod prefix has been changed to {prefix} successfully.");
            var dm = await C.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync($"{Context.Message.Author} changed everyone prefix to {prefix}.");
        }

        [Command("Admin")]
        [Remarks("Changes the Admin Prefix.")]
        public async Task AdminPrefix(string prefix)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            BotConfiguration botConfig = new BotConfiguration();
            botConfig.config.Admin_Prefix = prefix;
            botConfig.Serialize();

            await ReplyAsync($"The admin prefix has been changed to {prefix} successfully.");
            var dm = await C.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync($"{Context.Message.Author} changed everyone prefix to {prefix}.");
        }

        [Command("Owner")]
        [Remarks("Changes the Owner Prefix.")]
        public async Task OwnerPrefix(string prefix)
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var C = app.Owner;
            var O = Context.Guild.OwnerId;
            if (O != Context.Message.Author.Id || C.Id != Context.Message.Author.Id) return;

            BotConfiguration botConfig = new BotConfiguration();
            botConfig.config.Owner_Prefix = prefix;
            botConfig.Serialize();

            await ReplyAsync($"The owner prefix has been changed to {prefix} successfully.");
            var dm = await C.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync($"{Context.Message.Author} changed everyone prefix to {prefix}.");
        }
    }
}