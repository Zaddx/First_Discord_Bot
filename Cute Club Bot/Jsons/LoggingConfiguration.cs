using System.IO;
using Newtonsoft.Json;

namespace Cute_Club_Bot.Jsons
{
    public class LoggingConfiguration
    {
        public struct LogConfig
        {
            [JsonProperty("Log Type")]
            public string LogType { get; set; }

            [JsonProperty("Log Channel")]
            public string LogChannel { get; set; }
        }

        public LogConfig config;

        public LoggingConfiguration()
        {
            StreamReader r = new StreamReader("../../Jsons/loggingconfig.json");
            string json = r.ReadToEnd();
            LogConfig config = JsonConvert.DeserializeObject<LogConfig>(json);
            this.config.LogType = config.LogType;
            this.config.LogChannel = config.LogChannel;
            r.Close();
        }

        public void Serialize()
        {
            StreamWriter file = File.CreateText("../../Jsons/loggingconfig.json");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(file, config);
            file.Close();
        }
    }
}
