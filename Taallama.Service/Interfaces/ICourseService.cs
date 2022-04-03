using System;
using System.Linq;
using System.Threading.Tasks;
using Taallama.Domain.Commons;
using Taallama.Domain.Configurations;
using Taallama.Domain.Entities;
using Taallama.Service.DTOs;

namespace Taallama.Service.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<Course>> CreateAsync(CourseDTO courseDto);
        Task<BaseResponse<Course>> GetAsync(Guid id);
        Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseDTO courseDto);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<IQueryable<Course>>> GetAllAsync(PaginationParams @params);
    }
}