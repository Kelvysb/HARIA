using System.Threading.Tasks;

namespace HARIA.Common.Helpers
{
    public interface IHashHelper
    {
        Task<string> GetMD5(string input);
    }
}