using System.Threading.Tasks;

namespace Taallama.Data.IRepositories
{
    public interface IUnitOfWork : System.IDisposable
    {
        IUserRepository Users { get; }
        IVideoRepository Videos { get; }
        ICourseRepository Courses { get; }
        
        Task SaveChangesAsync();
    }
}