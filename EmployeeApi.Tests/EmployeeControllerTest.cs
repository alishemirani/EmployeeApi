using System.Threading.Tasks;
using System;
using Xunit;
using EmployeeApi.Services;
using Moq;
using EmployeeApi.Models;
using EmployeeApi.Controllers;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.DTO;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeeApi.Tests
{
    public class EmployeeControllerTest
    {
        private Guid validEmployeeId = Guid.NewGuid();
        private Guid invalidEmployeeId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();
        private Employee validEmployee;
        private FullEmployeeDTO validEmployeeDTO;
        private EmployeesController controller;

        public EmployeeControllerTest() {

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", userId.ToString())
            }, "mock"));

            var employeeService = new Mock<IEmployeeService>();
            employeeService.Setup(t => t.Find(userId, validEmployeeId))
            .Returns((Guid userId, Guid empId) => Task.FromResult(validEmployee));
            employeeService.Setup(t => t.Find(userId, invalidEmployeeId))
            .Returns((Guid userId, Guid empId) => Task.FromResult<Employee>(null));
            
            var mapper = new Mock<IMapper>();
            mapper.Setup(t=> t.Map<FullEmployeeDTO>(It.IsAny<Employee>()))
            .Returns((Employee e) => validEmployeeDTO);
            
            controller = new EmployeesController(mapper.Object, employeeService.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }


        [Fact]
        public async void TestValidGetById()
        {
            GivenEmployee();
            var result = await WhenGetEmployeeWithValidIdCalledAsync() ;
            Then200StatusCode(result as IStatusCodeActionResult);
            ThenValidEmployeeId(result as ObjectResult);
        }
        
        [Fact]
        public async void TestInValidGetById()
        {
            GivenEmployee();
            var result = await WhenGetEmployeeWithInValidIdCalledAsync() as IStatusCodeActionResult;
            Then404StatusCode(result);
        }

        private void Then404StatusCode(IStatusCodeActionResult result)
        {
            Assert.Equal(404, result.StatusCode.Value);
        }

        private void GivenEmployee() {
            validEmployee = new Employee(validEmployeeId);
            validEmployeeDTO = new FullEmployeeDTO() { Id = validEmployeeId };
        }
        private async Task<IActionResult> WhenGetEmployeeWithValidIdCalledAsync() {
            return await controller.GetEmployee(validEmployeeId);
        }

        private async Task<IActionResult> WhenGetEmployeeWithInValidIdCalledAsync() {
            return await controller.GetEmployee(invalidEmployeeId);
        }

        private void Then200StatusCode(IStatusCodeActionResult result) {
            Assert.Equal(200, result.StatusCode.Value);
        }

        private void ThenValidEmployeeId(ObjectResult objectResult) {
            Assert.Equal(validEmployeeId, (objectResult.Value as FullEmployeeDTO).Id);
        }
    }
}
