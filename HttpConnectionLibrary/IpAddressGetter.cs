using System.Net.Sockets;
using System.Net;
using System;

namespace HttpConnectionLibrary
{
    public class IpAddressGetter
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Локальный IP-адрес не найден.");
        }
    }
}
