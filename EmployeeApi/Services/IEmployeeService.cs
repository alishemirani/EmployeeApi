using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeApi.Models;

namespace EmployeeApi.Services
{
    public interface IEmployeeService : ICrudService<Employee, Guid>
    {
        Task<Employee> Find(Guid userId, Guid employeeId);
    }
}