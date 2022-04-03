using Taallama.Data.Contexts;
using Taallama.Data.IRepositories;
using Taallama.Domain.Entities;

namespace Taallama.Data.Repositories
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(TaallamaDbContext dbContext) : base(dbContext)
        { }
    }
}