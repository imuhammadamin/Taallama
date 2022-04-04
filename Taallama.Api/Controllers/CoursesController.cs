using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm]CourseDTO courseDto)
        {
            var result = await courseService.CreateAsync(courseDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> AddVideoAsync(Guid id, IEnumerable<VideoDTO> videos)
        {
            var result = await courseService.AddVideosAsync(id, videos);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get(Guid id)
        {
            var result = await courseService.GetAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(Guid id, CourseDTO courseDto)
        {
            var result = await courseService.UpdateAsync(id, courseDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await courseService.Where(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
