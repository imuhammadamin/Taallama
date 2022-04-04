using System;
using System.Linq;
using System.Threading.Tasks;
using Taallama.Domain.Commons;
using Taallama.Domain.Configurations;
using Taallama.Domain.Entities;
using Taallama.Service.DTOs;

namespace Taallama.Service.Interfaces
{
    public interface IVideoService
    {
        Task<BaseResponse<Video>> CreateAsync(VideoDTO videoDto);
        Task<BaseResponse<Video>> GetAsync(Guid id);
        Task<BaseResponse<Video>> UpdateAsync(Guid id, VideoDTO videoDTO);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<IQueryable<Video>>> Where(PaginationParams @params);
    }
}