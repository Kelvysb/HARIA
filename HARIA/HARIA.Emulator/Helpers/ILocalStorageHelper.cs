using System.Threading.Tasks;

namespace HARIA.Emulator.Helpers
{
    public interface ILocalStorageHelper
    {
        Task<T> GetItem<T>(string key);

        Task RemoveItem(string key);

        Task SetItem<T>(string key, T value);
    }
}