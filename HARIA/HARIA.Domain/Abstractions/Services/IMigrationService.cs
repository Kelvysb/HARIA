using System.Threading.Tasks;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IMigrationService
    {
        Task Migrate();
    }
}