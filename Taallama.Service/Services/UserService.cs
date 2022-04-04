using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taallama.Data.IRepositories;
using Taallama.Domain.Commons;
using Taallama.Domain.Configurations;
using Taallama.Domain.Entities;
using Taallama.Domain.Enums;
using Taallama.Service.DTOs;
using Taallama.Service.Extensions;
using Taallama.Service.Interfaces;

namespace Taallama.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration config, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.env = env;

            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>().ReverseMap()));
        }

        public async Task<BaseResponse<User>> CreateAsync(UserDTO userDto)
        {
            BaseResponse<User> response = new BaseResponse<User>();

            User existUser = await unitOfWork.Users.GetAsync(p => p.PhoneNumber == userDto.PhoneNumber);
            
            if (existUser is not null)
            {
                response.Error = new Error(409, "User already exists");
                return response;
            }
            else if (!userDto.Email.EndsWith("@gmail.com"))
            {
                response.Error = new Error(406, "Email must end with '@ gmail.com'");
                
                return response;
            }

            User mappedUser = mapper.Map<User>(userDto);

            mappedUser.Image = await FileExtensions
                .SaveFileAsync(userDto.ProfileImage.OpenReadStream(), userDto.ProfileImage.FileName, env, config);

            mappedUser.Create();

            User result = await unitOfWork.Users.CreateAsync(mappedUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var existUser = await unitOfWork.Users.GetAsync(p => p.Id == id && p.State != State.Deleted);
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

        public async Task<BaseResponse<IQueryable<User>>> Where(PaginationParams @params)
        {
            BaseResponse<IQueryable<User>> response = new();

            IQueryable<User> users = (await unitOfWork.Users.Where()).Where(p => p.State != State.Deleted);

            response.Data = users.ToPagedList(@params).AsQueryable();

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Guid id)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(p => p.Id == id);
            if (user is null)
            {
                response.Error = new Error(404, "User not found");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> UpdateAsync(Guid id, UserDTO userDto)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (user is null)
            {
                response.Error = new Error(404, "User not found");
                return response;
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.BirthDate = userDto.BirthDate;
            
            user.Update();

            var result = await unitOfWork.Users.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}