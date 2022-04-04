using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taallama.Domain.Commons;
using Taallama.Domain.Configurations;
using Taallama.Domain.Entities;
using Taallama.Service.DTOs;
using Taallama.Service.Interfaces;

namespace Taallama.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService videoService;
        
        public VideosController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Video>>> Create(VideoDTO videoDto)
        {
            var result = await videoService.CreateAsync(videoDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Get([FromRoute] Guid id)
        {
            var result = await videoService.GetAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Update([FromRoute] Guid id, VideoDTO videoDto)
        {
            var result = await videoService.UpdateAsync(id, videoDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await videoService.DeleteAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
        
        [HttpGet]
        public async Task<ActionResult<BaseResponse<IQueryable<Video>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await videoService.Where(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
