using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    public class Server : IHttpHandler, IDisposable
    {
        public event Action<object> OnGetData;

        private HttpListener _listener;
        public Server()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{8000}/"); ;
            _listener.Start();
        }

        public void Dispose()
        {
            _listener.Stop();
            _listener.Close();
        }

        public async Task UpdateData<T>(T obj)
        {
            await Console.Out.WriteLineAsync("Start updating data");
            var context = await _listener.GetContextAsync();
            await Console.Out.WriteLineAsync("Start getting request");
            var request = GetRequest<T>(context);

            await Console.Out.WriteLineAsync("Start sending response");
            SendResponse(context, obj);

            OnGetData?.Invoke(request);
        }

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
