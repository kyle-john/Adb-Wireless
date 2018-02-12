using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdbWireless
{
    public class AdbConnecter
    {
        static Regex regex = new Regex($"connected to (.+?){Environment.NewLine}");

        public static Task<bool> ConnectAsync(AndroidDevice device)
        {
            return ConnectAsync(device.IPAddress);
        }

        public static async Task<bool> ConnectAsync(string ipAddress)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = $"connect {ipAddress}",
                RedirectStandardOutput = true
            });

            var output = await process.StandardOutput.ReadToEndAsync();
            bool isSuccess = ParseAdbResponse(output);

            return isSuccess;
        }

        static bool ParseAdbResponse(string response)
        {
            return regex.IsMatch(response);
        }
    }
}
