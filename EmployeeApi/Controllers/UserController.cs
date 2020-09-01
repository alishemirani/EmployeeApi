using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Services;
using EmployeeApi.Models;
using EmployeeApi.Utilities;
using EmployeeApi.DTO;
using System;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase {
       private readonly IMapper mapper;
        private readonly IUserService userService;

        public UsersController(
            IMapper mapper,
            IUserService userService
            )
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        /// <summary>
        /// Get a list of all users, this action is only allowed for admins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<FullUserDTO>>), 200)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetAll();
            return Ok (new ApiResponse<IEnumerable<FullUserDTO>>() {
                Data = users.Select(t=> mapper.Map<FullUserDTO>(t))
            });
        }

        /// <summary>
        /// Find user by user id
        /// </summary>
        /// <param name="id">the user id</param>
        /// <returns>the user entity contains user information</returns>
        [HttpGet("{id}", Name="GetUser")]
        [ProducesResponseType(typeof(FullUserDTO), 200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = Policies.Admin)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await userService.GetById(id);
            if (user == null) {
                return NotFound();
            }
            return Ok(mapper.Map<FullUserDTO>(user));
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(FullUserDTO), 201)]
        public async Task<IActionResult> Createuser([FromBody] UserDTO userDTO) {
            var user = new User(Guid.NewGuid());
            mapper.Map(userDTO, user);
            await userService.Create(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, mapper.Map<FullUserDTO>(user));
        }


        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDTO userDTO) {
            var user = await userService.GetById(id);
            if (user == null) {
                return NotFound();
            }
            mapper.Map(userDTO, user);
            await userService.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(Guid id) {
            var user = await userService.GetById(id);
            if (user == null) {
                return NotFound();
            }
            await userService.Delete(user);
            return NoContent();
        }
    }
}