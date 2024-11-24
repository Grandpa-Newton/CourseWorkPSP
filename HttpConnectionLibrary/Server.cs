using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    /// <summary>
    /// Http-сервер для передачи данных
    /// </summary>
    public class Server : IHttpHandler
    {
        /// <summary>
        /// Событие, вызываемое после получения данных
        /// </summary>
        public event Action<object> OnGetData;

        /// <summary>
        /// Объект для http-сервера
        /// </summary>
        private HttpListener _listener;

        /// <summary>
        /// Создание Http-сервера
        /// </summary>
        public Server()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{8080}/"); ;
            _listener.Start();
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            _listener.Stop();
            _listener.Close();
        }

        /// <summary>
        /// Получение и отправка данных
        /// </summary>
        /// <typeparam name="T">Тип данных, передаваемых по сети</typeparam>
        /// <param name="obj">Данные, передаваемые по сети</param>
        /// <returns></returns>
        public async Task UpdateData<T>(T obj)
        {
            await Console.Out.WriteLineAsync("Start updating data");
            var context = await _listener.GetContextAsync();
            await Console.Out.WriteLineAsync("Start getting request");
            var request = GetRequest<T>(context);
            OnGetData?.Invoke(request);

            await Console.Out.WriteLineAsync("Start sending response");
            SendResponse(context, obj);
        }

        /// <summary>
        /// Очистка всех подписок на событие OnGetData
        /// </summary>
        public void ClearAllListeners()
        {
            OnGetData = null;
        }

        /// <summary>
        /// Получение запроса от клиента
        /// </summary>
        /// <typeparam name="T">Тип данных, передаваемых по сети</typeparam>
        /// <param name="context">Контекст подключения</param>
        /// <returns>Данные, полученные от клиента</returns>
        private T GetRequest<T>(HttpListenerContext context)
        {
            var request = context.Request;

            if (!request.HasEntityBody)
            {
                Console.WriteLine("Entity body is empty.");
                return default;
            }

            Stream stream = null;
            StreamReader streamReader = null;

            try
            {
                stream = request.InputStream;
                streamReader = new StreamReader(stream, request.ContentEncoding);
                string requestText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(requestText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                stream?.Close();
                streamReader?.Close();
            }
        }

        /// <summary>
        /// Отправка ответа на клиент
        /// </summary>
        /// <typeparam name="T">Тип данных, передаваемых по сети</typeparam>
        /// <param name="context">Контекст подключения</param>
        /// <param name="obj">Данные, передаваемые по сети</param>
        private void SendResponse<T>(HttpListenerContext context, T obj)
        {
            var response = context.Response;
            var dataText = JsonConvert.SerializeObject(obj);
            byte[] buffer = Encoding.UTF8.GetBytes(dataText);
            response.ContentLength64 = buffer.Length;

            using(Stream outputStream = response.OutputStream)
            {
                outputStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
