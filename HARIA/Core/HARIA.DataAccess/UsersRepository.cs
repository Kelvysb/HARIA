using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class UsersRepository : RepositoryBase<UserEntity>, IUsersRepository
    {
        public UsersRepository(IContext context) : base(context)
        {
        }
    }
}