using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpConnectionLibrary;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public class NetworkClientGetDataTest
    {
        private int _result;

        [TestMethod]
        public void NetworkDataSendingTestMethod()
        {
            var server = new Server();
            var client = new Client("localhost");
            var sendingData = 10;
            client.OnGetData += Client_OnGetData;

            client.GetData<int>();
            server.UpdateData(sendingData);

            Thread.Sleep(500);
            Assert.AreEqual(sendingData, _result);

            server.Dispose();
            client.Dispose();
        }
        private void Client_OnGetData(object obj)
        {
            _result = (int)obj;
        }
    }
}
