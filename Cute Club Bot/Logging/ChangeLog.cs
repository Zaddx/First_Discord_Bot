using System.IO;

namespace Cute_Club_Bot.Logging
{
    public class ChangeLog
    {
        public void LogChange(string message)
        {
            StreamWriter file = File.AppendText("../../Logging/changelog.txt");
            file.Write($"{message}\n");
            file.Close();
        }
    }
}
