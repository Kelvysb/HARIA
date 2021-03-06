using System.Threading.Tasks;

namespace HARIA.Emulator.Helpers
{
    public interface IHashHelper
    {
        Task<string> GetMD5(string input);
    }
}