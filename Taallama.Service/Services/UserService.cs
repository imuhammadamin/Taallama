using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taallama.Data.IRepositories;
using Taallama.Domain.Commons;
using Taallama.Domain.Configurations;
using Taallama.Domain.Entities;
using Taallama.Service.DTOs;
using Taallama.Service.Interfaces;

namespace Taallama.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            
            mapper = new Mapper
                (
                new MapperConfiguration
                    (
                        cfg => cfg.CreateMap<User, UserDTO>().ReverseMap()
                    )
                );
        }

        public async Task<BaseResponse<User>> CreateAsync(UserDTO userDto)
        {
            BaseResponse<User> response = new BaseResponse<User>();

            User existUser = await unitOfWork.Users.GetAsync(p => p.PhoneNumber == userDto.PhoneNumber);
            if (existUser is not null)
            {
                // Error code is User already exist
                response.Error = new Error(409, "User already exists");
                return response;
            }

            User mappedUser = mapper.Map<User>(userDto);

            User result = await unitOfWork.Users.CreateAsync(mappedUser);

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var existUser = await unitOfWork.Users.GetAsync(p => p.Id == id);
            if (existUser is null)
            {
                response.Error = new Error(404, "User not found");
                return response;
            }

            existUser.Delete();

            var result = await unitOfWork.Users.UpdateAsync(existUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public Task<BaseResponse<IQueryable<User>>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> UpdateAsync(Guid id, UserDTO userDto)
        {
            throw new NotImplementedException();
        }
    }
}