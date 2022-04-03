using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

            mappedCourse.Thumbnail = await SaveFileAsync(courseDto.Thumbnail.OpenReadStream(), courseDto.Thumbnail.FileName);

            mappedCourse.Create();

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

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

        public async Task<BaseResponse<IQueryable<Course>>> GetAllAsync(PaginationParams @params)
        {
            BaseResponse<IQueryable<Course>> response = new();

            IQueryable<Course> courses = (await unitOfWork.Courses.GetAllAsync()).Where(p => p.State != State.Deleted);

            response.Data = courses.ToPagedList(@params).AsQueryable();

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

            response.Data = course;

            return response;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + '_' + fileName;

            string storagePath = config.GetSection("Storage:ImageUrl").Value;

            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");

            FileStream mainFile = File.Create(filePath);

            await file.CopyToAsync(mainFile);

            mainFile.Close();

            return fileName;
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
            

            course.Update();

            var result = await unitOfWork.Courses.UpdateAsync(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}