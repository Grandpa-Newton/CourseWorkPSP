using System.Net.Sockets;
using System.Net;
using System;

namespace HttpConnectionLibrary
{
    /// <summary>
    /// Класс для получения Ip-адреса
    /// </summary>
    public class IpAddressGetter
    {
        /// <summary>
        /// Получение Ip-адреса компьютера
        /// </summary>
        /// <returns>Ip-адрес компьютера</returns>
        /// <exception cref="Exception">Ошибка, возникающая при невозможности найти Ip-адрес</exception>
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
