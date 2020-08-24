using System;
using EmployeeApi.Models;

namespace EmployeeApi.Repository {
    public interface IEmployeeRepository : ICrudRepository<Employee, Guid> 
    {

    }
}