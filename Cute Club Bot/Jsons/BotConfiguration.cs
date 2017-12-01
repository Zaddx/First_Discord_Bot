using System.IO;
using Newtonsoft.Json;

namespace Cute_Club_Bot.Jsons
{
    public class BotConfiguration
    {
        public struct Configuration
        {
            [JsonProperty("Owner Prefix")]
            public string Owner_Prefix { get; set; }

            [JsonProperty("Admin Prefix")]
            public string Admin_Prefix { get; set; }

            [JsonProperty("Mod Prefix")]
            public string Mod_Prefix { get; set; }

            [JsonProperty("Everyone Prefix")]
            public string Everyone_Prefix { get; set; }

            [JsonProperty("Invite Channel")]
            public string Invite_Channel { get; set; }
        }

        public Configuration config;

        public BotConfiguration()
        {
            StreamReader r = new StreamReader("../../Jsons/config.json");
            string json = r.ReadToEnd();
            Configuration tempConfig = JsonConvert.DeserializeObject<Configuration>(json);
            this.config.Owner_Prefix = tempConfig.Owner_Prefix;
            this.config.Admin_Prefix = tempConfig.Admin_Prefix;
            this.config.Mod_Prefix   = tempConfig.Mod_Prefix;
            this.config.Everyone_Prefix = tempConfig.Everyone_Prefix;
            this.config.Invite_Channel = tempConfig.Invite_Channel;
            r.Close();
        }

        public void Serialize()
        {
            StreamWriter file = File.CreateText("../../Jsons/config.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(file, config);
            file.Close();
        }
    }
}
