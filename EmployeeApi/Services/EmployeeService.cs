using System;
using System.Threading.Tasks;
using EmployeeApi.Models;
using EmployeeApi.Repository;
using Microsoft.Extensions.Logging;

namespace EmployeeApi.Services
{
    public class EmployeeService : CrudService<Employee, Guid>, IEmployeeService
    {
        private readonly IUserRepository userRepository;

        public EmployeeService(
            ILogger<EmployeeService> logger,
            IEmployeeRepository employeeRepository,
            IUserRepository userRepository) : base(logger, employeeRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Employee> Find(Guid userId, Guid employeeId)
        {
            logger.LogInformation($"Find employee with userId: {userId} and employeeId: {employeeId} ");
            var user = await userRepository.GetById(userId);
            var employee = await repository.GetById(employeeId);
            if (user.HasRole(Policies.Admin)) {
                return await Task.FromResult(employee);
            }
            return (employee.UserId == userId ? await Task.FromResult(employee) : await Task.FromResult<Employee>(null));
        }
    }
}