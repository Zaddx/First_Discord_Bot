using Newtonsoft.Json;
using System.IO;

namespace Cute_Club_Bot.Jsons
{
    public class BotSettings
    {
        public struct Bot_Info
        {
            [JsonProperty("Token")]
            public string Token { get; set; }

            [JsonProperty("Nickname")]
            public string Nickname { get; set; }

            [JsonProperty("Avatar")]
            public string Avatar { get; set; }
        }

        public Bot_Info botSettings;

        public BotSettings()
        {
            StreamReader r = new StreamReader("../../Jsons/botsettings.json");
            string json = r.ReadToEnd();
            Bot_Info settings = JsonConvert.DeserializeObject<Bot_Info>(json);
            botSettings.Token = settings.Token;
            botSettings.Nickname = settings.Nickname;
            botSettings.Avatar = settings.Avatar;
            r.Close();
        }

        public void Serialize()
        {
            StreamWriter file = File.CreateText("../../Jsons/botsettings.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, botSettings);
            file.Close();
        }
    }
}
