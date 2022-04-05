using System;
using System.Collections.Generic;
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
        Task<BaseResponse<IEnumerable<Course>>> Where(PaginationParams @params);
        Task<BaseResponse<Course>> AddVideosAsync(Guid id, IEnumerable<VideoDTO> videos);
    }
}