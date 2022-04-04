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
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<User>>> Create([FromQuery]Login login, UserDTO userDto)
        {
            var result = await userService.CreateAsync(login, userDto);
            
            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Get([FromRoute] Guid id)
        {
            var result = await userService.GetAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Update([FromRoute]Guid id, UserDTO userDto)
        {
            var result = await userService.UpdateAsync(id, userDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await userService.DeleteAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IQueryable<User>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await userService.Where(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}