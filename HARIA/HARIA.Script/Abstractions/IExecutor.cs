using HARIA.Domain.Entities;
using HARIA.Script.Enums;
using HARIA.Script.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARIA.Script.Abstractions
{
    public interface IExecutor
    {
        Task<Variable> Get(SourceGroup group, string id, string path);

        Task Set<T>(SourceGroup group, T entity) where T: EntityBase;

        Task Schedule(string id, double time, TimeUnit timeUnit, string command, List<Variable> variables);

        Task<List<DeviceDataEntity>> ListDevices();

        Task<List<Schedule>> ListSchedules();

        Task<List<BlackboardEntity>> ListBlackboard();

        Task<BlackboardEntity> GetBlackboardValue(string key);

        Task SetBlackboardValue(string key, string value);
    }
}
