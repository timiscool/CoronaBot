using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class CoronaBot : IBot
    {

        private DiscordClient Client { get; set; }
        private CommandsNextExtension Commands { get; set; }
        private string _prefix;
        private string _token;



        public async Task RunAsync()
        {
            
            var loaded = LoadConfigAsync().GetAwaiter().GetResult();

            if (loaded)
            {
                var config = new DiscordConfiguration()
                {
                    Token = _token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    LogLevel = LogLevel.Debug,
                    UseInternalLogHandler = true
                };

                Client = new DiscordClient(config);
                Client.Ready += onClientReady;

                var commandsConfig = new CommandsNextConfiguration()
                {
                    StringPrefixes = new string[] { _prefix },
                    EnableDms = false,
                    EnableMentionPrefix = true,
                    DmHelp = false,
                    IgnoreExtraArguments = false
                };

                Commands = Client.UseCommandsNext(commandsConfig);
                Commands.RegisterCommands<FirstCommand>();


                await Client.ConnectAsync();
                // wait for commands
                await Task.Delay(-1);
            }

        }

        private async Task<bool> LoadConfigAsync()
        {
            var json = String.Empty;
            using (var fs = File.OpenRead(""))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            _token = configJson.Token;
            _prefix = configJson.Prefix;

            return _token != null & _prefix != null;
        }




        // when async, use Task as void, I don't want data back
        private Task onClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;

        }
       
    }
}
