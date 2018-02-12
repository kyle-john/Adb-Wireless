using System;

namespace AdbWireless.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var isSuccess = AdbConnecter.Connect("192.168.219.104").Result;
        }
    }
}
