using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;

        public CourseService(IUnitOfWork unitOfWork, IConfiguration config, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.env = env;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseDTO courseDto)
        {
            var response = new BaseResponse<Course>();

            Course course = await unitOfWork.Courses.GetAsync
                (p => p.Title == courseDto.Title && p.CourseOwnerId == courseDto.CourseOwnerId && p.State != State.Deleted);

            if (course is not null)
            {
                response.Error = new Error(409, "Course already exists");
                return response;
            }

            var mappedCourse = new Course();
            mappedCourse.Title = courseDto.Title;
            mappedCourse.Description = courseDto.Description;
            mappedCourse.CourseOwnerId = courseDto.CourseOwnerId;

            mappedCourse.Thumbnail = await FileExtensions.SaveFileAsync(courseDto.Thumbnail.OpenReadStream(), courseDto.Thumbnail.FileName, env, config);

            mappedCourse.Create();

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);
            result.CourseOwner = await unitOfWork.Users.GetAsync(p => p.Id == result.CourseOwnerId);

            result.Thumbnail = "https://localhost:5001/Images/" + result.Thumbnail;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<Course>> AddVideosAsync(Guid id, IEnumerable<VideoDTO> videos)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (course is null)
            {
                response.Error = new Error(404, "Course not found");
                return response;
            }

            foreach (var videoDto in videos)
            {
                Video video = new Video()
                {
                    Title = videoDto.Title,
                    CourseId = videoDto.CourseId
                };
                video.Create();

                course.Videos.Add(video);
            }

            var result = await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (course is null)
            {
                response.Error = new Error(404, "Course not found");
                return response;
            }

            course.Delete();

            var result = await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Course>>> Where(PaginationParams @params)
        {
            BaseResponse<IEnumerable<Course>> response = new();

            IEnumerable<Course> courses = (await unitOfWork.Courses.Where(p => p.State != State.Deleted))
                .Include(o => o.CourseOwner)
                .Include(p => p.Videos);

            response.Data = courses.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Guid id)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id);
            if (course is null)
            {
                response.Error = new Error(404, "Course not found");
                return response;
            }

            course.CourseOwner = await unitOfWork.Users.GetAsync(p => p.Id == course.CourseOwnerId);

            course.Videos = (await unitOfWork.Videos.Where(p => p.CourseId == course.Id)).ToList();

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseDTO courseDto)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (course is null)
            {
                response.Error = new Error(404, "Course not found");
                return response;
            }

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.Thumbnail = await FileExtensions
                .SaveFileAsync(courseDto.Thumbnail.OpenReadStream(), courseDto.Thumbnail.FileName, env, config);


            course.Update();

            var result = await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}