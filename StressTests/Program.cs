using GameLibrary;
using HttpConnectionLibrary;
using System;
using System.Diagnostics;
using System.Linq;

namespace StressTests
{
    internal class Program
    {
        private static NetworkData _clientResult;
        private static NetworkData _serverResult;

        private static Stopwatch _clientStopwatch;
        private static Stopwatch _serverStopwatch;

        private const int IterationsNumber = 1000;

        private static double[] clientsTimes;
        private static double[] serversTimes;

        static void Main(string[] args)
        {
            clientsTimes = new double[IterationsNumber];
            serversTimes = new double[IterationsNumber];

            for (int i = 0; i < IterationsNumber; i++)
            {
                TestDataSending(i);
            }

            Console.WriteLine($"Среднее количество микросекунд для клиентов: {clientsTimes.Average()}");
            Console.WriteLine($"Среднее количество микросекунд для серверов: {serversTimes.Average()}");
        }

        private static void TestDataSending(int iterationNumber)
        {
            var server = new Server();
            var client = new Client("localhost");
            client.OnGetData += Client_OnGetData;
            server.OnGetData += Server_OnGetData;
            NetworkData clientNetworkData = new NetworkData()
            {
                Fuel = 1000
            };
            NetworkData serverNetworkData = new NetworkData()
            {
                Fuel = 2000
            };

            _clientResult = null;
            _serverResult = null;

            _clientStopwatch = new Stopwatch();
            _clientStopwatch.Start();
            client.UpdateData(clientNetworkData);


            _serverStopwatch = new Stopwatch();
            _serverStopwatch.Start();
            server.UpdateData(serverNetworkData);

            while (_clientResult == null || _serverResult == null)
            {

            }

            var clientTime = _clientStopwatch.ElapsedTicks * (1000000 / (double)Stopwatch.Frequency);
            clientsTimes[iterationNumber] = clientTime;

            Console.WriteLine($"Результат, полученный клиентом от сервера: {_clientResult.Fuel}");
            Console.WriteLine($"Время, потраченное клиентом на отправку и получение данных от сервера: {clientTime} микросекунд");

            var serverTime = _serverStopwatch.ElapsedTicks * (1000000 / (double)Stopwatch.Frequency);
            serversTimes[iterationNumber] = serverTime;

            Console.WriteLine($"Результат, полученный клиентом от сервера: {_serverResult.Fuel}");
            Console.WriteLine($"Время, потраченное сервером на отправку и получение данных от клиента: {serverTime} микросекунд");

            server.ClearAllListeners();
            client.ClearAllListeners();

            server.Dispose();
            client.Dispose();
        }

        private static void Server_OnGetData(object obj)
        {
            _serverResult = (NetworkData)obj;
            _serverStopwatch?.Stop();
        }

        private static void Client_OnGetData(object obj)
        {
            _clientResult = (NetworkData)obj;
            _clientStopwatch?.Stop();
        }
    }
}
