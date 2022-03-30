using AlgorhythmService.Shared.UnitTest;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AlgorhythmService.Shared.LogShared
{
    public class LogShared
    {
        public static async Task LogInformationAsync(string message)
        {
            await Log(message, "Information");
        }

        public static async Task LogErrorAsync(Exception ex)
        {
            var message = $@"
Message: {ex.Message}
StackTrace: {ex.StackTrace}";

            await Log(message, "Error");
        }

        private static async Task Log(string message, string arquiveName)
        {
            Directory.CreateDirectory("Logs");

            if (!UnitTestDetector.IsRunningUnitTest)
            {
                using (StreamWriter w = File.AppendText($"Logs\\{arquiveName}_{DateTime.Today.ToString("dd-MM-yyyy")}_logs.txt"))
                {
                    await w.WriteLineAsync(message);
                }
            }
        }
    }
}
