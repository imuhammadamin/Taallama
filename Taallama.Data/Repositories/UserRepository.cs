using Taallama.Data.Contexts;
using Taallama.Data.IRepositories;
using Taallama.Domain.Entities;

namespace Taallama.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TaallamaDbContext dbContext) : base(dbContext)
        { }
    }
}
