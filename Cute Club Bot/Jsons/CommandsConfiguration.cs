using System.IO;
using Newtonsoft.Json;

namespace Cute_Club_Bot.Jsons
{
    public class CommandsConfiguration
    {
        public struct CommandsConfig
        {
            [JsonProperty("Change Avatar")]
            public bool Change_Avatar { get; set; }

            [JsonProperty("Change Nickname")]
            public bool Change_Nickname { get; set; }

            [JsonProperty("Change Prefix")]
            public bool Change_Prefix { get; set; }

            [JsonProperty("Change Logging Type")]
            public bool Change_Logging_Type { get; set; }

            [JsonProperty("Change Logging Channel")]
            public bool Change_Logging_Channel { get; set; }

            [JsonProperty("Change Invite Channel")]
            public bool Change_Invite_Channel { get; set; }

            [JsonProperty("Create Instant Invite")]
            public bool Create_Instant_Invite { get; set; }

            [JsonProperty("Logging")]
            public bool Logging { get; set; }
        }

        public CommandsConfig config;

        public CommandsConfiguration()
        {
            StreamReader r = new StreamReader("../../Jsons/commands.json");
            string json = r.ReadToEnd();
            CommandsConfig tempConfig = JsonConvert.DeserializeObject<CommandsConfig>(json);
            this.config.Change_Avatar = tempConfig.Change_Avatar;
            this.config.Change_Nickname = tempConfig.Change_Nickname;
            this.config.Change_Prefix = tempConfig.Change_Prefix;
            this.config.Change_Invite_Channel = tempConfig.Change_Invite_Channel;
            this.config.Change_Logging_Type = tempConfig.Change_Logging_Type;
            this.config.Change_Logging_Channel = tempConfig.Change_Logging_Channel;
            this.config.Create_Instant_Invite = tempConfig.Create_Instant_Invite;
            this.config.Logging = tempConfig.Logging;
            r.Close();
        }

        public void Serialize()
        {
            StreamWriter file = File.CreateText("../../Jsons/commands.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(file, config);
            file.Close();
        }
    }
}
