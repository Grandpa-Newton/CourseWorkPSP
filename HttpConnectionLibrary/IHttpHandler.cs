using System;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    /// <summary>
    /// Интерфейс для передачи данных по сети
    /// </summary>
    public interface IHttpHandler : IDisposable
    {
        /// <summary>
        /// Метод для отправки и получения данных
        /// </summary>
        /// <typeparam name="T">Тип передаваемых данных</typeparam>
        /// <param name="obj">Данные, передаваемые по сети</param>
        /// <returns>Данные, полученнные от второго узла</returns>
        Task UpdateData<T>(T obj);

        /// <summary>
        /// Событие, вызываемое при получении данных
        /// </summary>
        event Action<object> OnGetData;

        /// <summary>
        /// Очистка всех подписок на событие OnGetData
        /// </summary>
        void ClearAllListeners();
    }
}
