using System;
using Discord;
using System.IO;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Cute_Club_Bot.Jsons;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cute_Club_Bot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private ICommandContext _context;

        // Get the bot settings
        BotSettings _botSettings = new BotSettings();

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                //(WebSocketProvider = WS4NetProvider.Instance,)
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 1000,
            });
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // Event Subscription
            _client.Log += Log;
            _client.MessageDeleted += MessageDeletedLogger;
            _client.MessageUpdated += MessageUpdatedLogger;

            await _client.SetGameAsync("Murder Simulator");

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, _botSettings.settings.Token);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task MessageUpdatedLogger(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            StreamWriter file = File.AppendText("../../Logging/editlog.txt");
            file.Write($"[{arg1.Value.Timestamp}] {arg1.Value.Author}: {arg1.Value.Content}");
            file.Close();

            await Task.Delay(1);
        }

        private async Task MessageDeletedLogger(Cacheable<IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            StreamWriter file = File.AppendText("../../Logging/deletionlog.txt");
            file.Write($"[{arg1.Value.Timestamp}] {arg1.Value.Author}: {arg1.Value.Content}\n");
            file.Close();

            await Task.Delay(1);
        }

        private async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix(new BotConfiguration().config.Everyone_Prefix, ref argPos) ||
                message.HasStringPrefix(new BotConfiguration().config.Mod_Prefix, ref argPos) ||
                message.HasStringPrefix(new BotConfiguration().config.Admin_Prefix, ref argPos) ||
                message.HasStringPrefix(new BotConfiguration().config.Owner_Prefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                _context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(_context, argPos, _services);

                if (!result.IsSuccess)
                    Console.Write(result.ErrorReason);
            }
        }

        private Task Log(LogMessage message)
        {
            var consoleColor = Console.ForegroundColor;
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message}");
            Console.ForegroundColor = consoleColor;


            return Task.CompletedTask;
        }
    }
}
