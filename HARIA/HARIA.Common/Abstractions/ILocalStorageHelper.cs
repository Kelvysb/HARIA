using System.Threading.Tasks;

namespace HARIA.Common.Abstractions
{
    public interface ILocalStorageHelper
    {
        Task<T> GetItem<T>(string key);

        Task RemoveItem(string key);

        Task SetItem<T>(string key, T value);
    }
}