using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(Context context) : base(context)
        {
        }
    }
}