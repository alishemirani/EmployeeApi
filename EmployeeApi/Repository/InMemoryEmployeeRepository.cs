using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> employeeList = new List<Employee>();
        public Task<bool> Delete(Employee entity)
        {
            return Task.FromResult(employeeList.Remove(entity));
        }

        public Task<IEnumerable<Employee>> GetAll()
        {
            return Task.FromResult(employeeList.AsEnumerable());
        }

        public Task<Employee> GetById(Guid id)
        {
            return Task.FromResult(employeeList.Where(t=> t.Id == id).FirstOrDefault());
        }

        public Task<Employee> Insert(Employee entity)
        {
            employeeList.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> Update(Employee entity)
        {
            var index = employeeList.FindIndex(t=>t.Id == entity.Id);
            if (index < 0)
                return Task.FromResult(false);
            employeeList[index] = entity;
            return Task.FromResult(true);
        }
    }
}