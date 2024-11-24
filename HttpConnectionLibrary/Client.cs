using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    /// <summary>
    /// Http-клиент для передачи данных
    /// </summary>
    public class Client : IHttpHandler
    {
        /// <summary>
        /// Событие, вызываемое после получения данных
        /// </summary>
        public event Action<object> OnGetData;

        /// <summary>
        /// Объект http-клиента
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// Адрес для подсоединения
        /// </summary>
        private string _address;

        /// <summary>
        /// Конструктор для создания http-клиента
        /// </summary>
        /// <param name="ipAddress">Ip-адрес для подключения</param>
        public Client(string ipAddress)
        {
            _httpClient = new HttpClient();
            _address = $"http://{ipAddress}:8080/";
        }
        
        /// <summary>
        /// Получение данных, отправленных по сети
        /// </summary>
        /// <typeparam name="T">Тип полученных данных</typeparam>
        /// <returns></returns>
        public async Task GetData<T>()
        {
            var response = await _httpClient.GetAsync(_address);
            var resultText = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultText);
            OnGetData?.Invoke(result);
        }

        /// <summary>
        /// Отправка и получение данных
        /// </summary>
        /// <typeparam name="T">Тип данных, передававемых по сети</typeparam>
        /// <param name="obj">Объект данных, отправляемых по сети</param>
        /// <returns></returns>
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

        /// <summary>
        /// Очистка всех подписок на событие OnGetData
        /// </summary>
        public void ClearAllListeners()
        {
            OnGetData = null;
        }

        /// <summary>
        /// Освобождение всех ресурсов
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
