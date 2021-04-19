using System;
using System.Threading.Tasks;

namespace HARIA.Domain.Abstractions.Services
{
    public interface ITimeService
    {
        public Task<DateTime> GetTime();
    }
}