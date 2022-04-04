using System;
using System.Linq;
using System.Threading.Tasks;
using Taallama.Domain.Commons;
using Taallama.Domain.Configurations;
using Taallama.Domain.Entities;
using Taallama.Service.DTOs;

namespace Taallama.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserDTO userDto);
        Task<BaseResponse<User>> GetAsync(Guid id);
        Task<BaseResponse<User>> UpdateAsync(Guid id, UserDTO userDto);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<IQueryable<User>>> Where(PaginationParams @params);
    }
}