using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdbWireless.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var devices = await AndroidDeviceBrowser.BrowseAsync();
                if (devices.Count == 0)
                {
                    Console.WriteLine("Fail to browse connectable android devices.");
                    return;
                }

                Console.WriteLine("Select an android device want to connect.");
                foreach ((var device, var index) in devices.Select((device, index) => (device, index)))
                {
                    Console.WriteLine($"{index + 1}: {device.Name}");
                }

                bool parsed = int.TryParse(Console.ReadLine(), out int number);
                if (!parsed) number = 1;

                var connectingDevice = devices[number - 1];
                bool connected = await AdbConnecter.ConnectAsync(connectingDevice);

                Console.WriteLine($"{(connected ? "" : "Fail to ")}Connect to {connectingDevice.Name}");
            }).Wait();
            Console.ReadLine();
        }
    }
}
