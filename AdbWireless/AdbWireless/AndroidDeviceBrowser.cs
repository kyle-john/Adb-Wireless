using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeroconf;

namespace AdbWireless
{
    public static class AndroidDeviceBrowser
    {
        public static async Task<IReadOnlyList<AndroidDevice>> BrowseAsync(Action<AndroidDevice> callback = null)
        {
            Action<IZeroconfHost> action = null;
            if (callback != null)
            {
                action = host => callback(host.ToAndroidDevice());
            }

            var hosts = await ZeroconfResolver.ResolveAsync("_adbwireless._tcp.local.", callback: action);

            var devices = hosts.Select(host => host.ToAndroidDevice()).ToList();

            return devices;
        }

        static AndroidDevice ToAndroidDevice(this IZeroconfHost host)
        {
            return new AndroidDevice
            {
                Name = host.DisplayName,
                IPAddress = host.IPAddress
            };
        }
    }
}
