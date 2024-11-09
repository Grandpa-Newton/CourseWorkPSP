using System;
using System.Threading.Tasks;

namespace HttpConnectionLibrary
{
    public interface IHttpHandler : IDisposable
    {
        Task UpdateData<T>(T obj);
        event Action<object> OnGetData;
        void ClearAllListeners();
    }
}
