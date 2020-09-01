using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using System.Threading.Tasks;
using System;
using AutoMapper;
using System.Security.Claims;
using EmployeeApi.Services;
using EmployeeApi.DTO;
using System.Linq;
using EmployeeApi.Utilities;
using System.Collections.Generic;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public EmployeesController(
            IMapper mapper,
            IEmployeeService employeeService
            )
        {
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Get a list of all employees, this action is only allowed for admins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<FullEmployeeDTO>>), 200)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await employeeService.GetAll();
            return Ok (new ApiResponse<IEnumerable<FullEmployeeDTO>>() {
                Data = employees.Select(t=> mapper.Map<FullEmployeeDTO>(t))
            });
        }

        /// <summary>
        /// Find employee by employee id
        /// </summary>
        /// <param name="id">the employee id</param>
        /// <returns>the employee entity contains employee information</returns>
        [HttpGet("{id}", Name="GetEmployee")]
        [ProducesResponseType(typeof(FullEmployeeDTO), 200)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId"));
            var employee = await employeeService.Find(userId, id);
            if (employee == null) {
                return NotFound();
            }
            return Ok(mapper.Map<FullEmployeeDTO>(employee));
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(FullEmployeeDTO), 201)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDTO) {
            var employee = new Employee(Guid.NewGuid());
            mapper.Map(employeeDTO, employee);
            await employeeService.Create(employee);
            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, mapper.Map<FullEmployeeDTO>(employee));
        }


        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDTO employeeDTO) {
            var employee = await employeeService.GetById(id);
            if (employee == null) {
                return NotFound();
            }
            mapper.Map(employeeDTO, employee);
            await employeeService.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteEmployee(Guid id) {
            var employee = await employeeService.GetById(id);
            if (employee == null) {
                return NotFound();
            }
            await employeeService.Delete(employee);
            return NoContent();
        }
    }
}