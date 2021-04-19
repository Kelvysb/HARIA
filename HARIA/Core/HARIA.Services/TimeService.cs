using System;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Constants;

namespace HARIA.Services
{
    public class TimeService : ITimeService
    {
        private readonly IStatesService statesService;

        public TimeService(IStatesService statesService)
        {
            this.statesService = statesService;
        }

        public async Task<DateTime> GetTime()
        {
            var timeMode = await statesService.GetState(StateDefaultKeys.TIME_MODE);
            var offSet = 0;
            if (timeMode.Value == TimeMode.SIMULATED)
            {
                var timeOffset = await statesService.GetState(StateDefaultKeys.TIME_OFFSET);
                int.TryParse(timeOffset.Value, out offSet);
            }
            return DateTime.Now.AddMinutes(offSet);
        }
    }
}