using System.Threading.Tasks;

namespace HARIA.Domain.Abstractions
{
    public interface IHashHelper
    {
        Task<string> GetMD5(string input);
    }
}