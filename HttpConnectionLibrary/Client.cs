using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    public class Client : IHttpHandler, IDisposable
    {
        public event Action<object> OnGetData;

        private HttpClient _httpClient;
        private string _address;

        public Client(string ipAddress)
        {
            _httpClient = new HttpClient(new HttpClientHandler());
            _address = $"http://{ipAddress}:8000/";
        }
        
        public async Task GetData<T>()
        {
            Console.WriteLine("Start getting data");
            var response = await _httpClient.GetAsync(_address);
            Console.WriteLine("Start reading data");
            var resultText = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultText);
            OnGetData?.Invoke(result);
        }

        public async Task UpdateData<T>(T obj)
        {
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
