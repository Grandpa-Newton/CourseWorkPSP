using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpConnectionLibrary;
using System.Threading;
using GameLibrary;

namespace UnitTests
{
    [TestClass]
    public class NetworkUpdateDataTest
    {
        private NetworkData _clientResult;
        private NetworkData _serverResult;

        [TestMethod]
        public void NetworkDataSendingTestMethod()
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

            client.UpdateData(clientNetworkData);
            server.UpdateData(serverNetworkData);
            Thread.Sleep(1000);

            Assert.AreEqual(serverNetworkData.Fuel, _clientResult.Fuel);
            Assert.AreEqual(clientNetworkData.Fuel, _serverResult.Fuel);

            server.Dispose();
            client.Dispose();
        }

        private void Server_OnGetData(object obj)
        {
            _serverResult = (NetworkData)obj;
        }

        private void Client_OnGetData(object obj)
        {
            _clientResult = (NetworkData)obj;
        }
    }
}
