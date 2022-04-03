using Taallama.Data.Contexts;
using Taallama.Data.IRepositories;
using Taallama.Domain.Entities;

namespace Taallama.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(TaallamaDbContext dbContext) : base(dbContext)
        { }
    }
}