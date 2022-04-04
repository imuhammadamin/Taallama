using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VideoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            mapper = new Mapper
                (
                new MapperConfiguration
                    (
                        cfg => cfg.CreateMap<Video, VideoDTO>().ReverseMap()
                    )
                );
        }

        public async Task<BaseResponse<Video>> CreateAsync(VideoDTO videoDto)
        {
            var response = new BaseResponse<Video>();

            Video video = await unitOfWork.Videos.GetAsync
                (p => p.Title == videoDto.Title && p.CourseId == videoDto.CourseId && p.State != State.Deleted);
            
            if (video is not null)
            {
                response.Error = new Error(409, "Video already exists");
                return response;
            }

            Video mappedVideo = mapper.Map<Video>(videoDto);

            mappedVideo.Create();

            Video result = await unitOfWork.Videos.CreateAsync(mappedVideo);
            
            result.Course = await unitOfWork.Courses.GetAsync(p => p.Id == result.CourseId);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            var response = new BaseResponse<bool>();

            var existVideo = await unitOfWork.Videos.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (existVideo is null)
            {
                response.Error = new Error(404, "Video not found");
                return response;
            }

            existVideo.Delete();

            var result = await unitOfWork.Videos.UpdateAsync(existVideo);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IQueryable<Video>>> Where(PaginationParams @params)
        {
            BaseResponse<IQueryable<Video>> response = new();

            IQueryable<Video> videos = (await unitOfWork.Videos.Where()).Where(p => p.State != State.Deleted).Include(p => p.Course);

            response.Data = videos.ToPagedList(@params).AsQueryable();

            return response;
        }

        public async Task<BaseResponse<Video>> GetAsync(Guid id)
        {
            var response = new BaseResponse<Video>();

            var video = await unitOfWork.Videos.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (video is null)
            {
                response.Error = new Error(404, "Video not found");
                return response;
            }

            video.Course = await unitOfWork.Courses.GetAsync(p => p.Id == video.CourseId);

            response.Data = video;

            return response;
        }

        public async Task<BaseResponse<Video>> UpdateAsync(Guid id, VideoDTO videoDto)
        {
            var response = new BaseResponse<Video>();

            var video = await unitOfWork.Videos.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (video is null)
            {
                response.Error = new Error(404, "Video not found");
                return response;
            }

            video.Title = videoDto.Title;
            video.CourseId = videoDto.CourseId;
            
            video.Update();

            var result = await unitOfWork.Videos.UpdateAsync(video);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}