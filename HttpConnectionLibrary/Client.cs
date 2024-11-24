using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    public class Client : IHttpHandler
    {
        public event Action<object> OnGetData;

        private HttpClient _httpClient;
        private string _address;

        public Client(string ipAddress)
        {
            _httpClient = new HttpClient();
            _address = $"http://{ipAddress}:8080/";
        }
        
        public async Task GetData<T>()
        {
            var response = await _httpClient.GetAsync(_address);
            var resultText = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultText);
            OnGetData?.Invoke(result);
        }

        public async Task UpdateData<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            var response = await _httpClient.PostAsync(_address, data);
            string resultText = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(resultText);

            OnGetData?.Invoke(result);
        }

        public void ClearAllListeners()
        {
            OnGetData = null;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
