using System.Threading.Tasks;

namespace HARIA.Common.Abstractions
{
    public interface IHashHelper
    {
        Task<string> GetMD5(string input);
    }
}