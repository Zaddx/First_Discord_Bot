using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Cute_Club_Bot.Jsons;

namespace Cute_Club_Bot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService      _commands;
        private IServiceProvider    _services;

        // Get the bot settings
        BotSettings _botSettings = new BotSettings();

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                //(WebSocketProvider = WS4NetProvider.Instance,)
                LogLevel = LogSeverity.Verbose
            });
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // Event Subscription
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, _botSettings.botSettings.Token);

            await _client.StartAsync();

            await Task.Delay(-1);
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

            if (message.HasStringPrefix("t!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

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
