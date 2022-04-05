using System.Threading.Tasks;
using Taallama.Data.Contexts;
using Taallama.Data.IRepositories;

namespace Taallama.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private TaallamaDbContext context;

        public IUserRepository Users { get; private set; }
        public IVideoRepository Videos { get; private set; }
        public ICourseRepository Courses { get; private set; }

        public UnitOfWork(TaallamaDbContext context)
        {
            this.context = context;

            Users = new UserRepository(context);
            Videos = new VideoRepository(context);
            Courses = new CourseRepository(context);
        }

        public void Dispose() =>
            System.GC.SuppressFinalize(this);

        public async Task SaveChangesAsync() =>
            await context.SaveChangesAsync();

    }
}