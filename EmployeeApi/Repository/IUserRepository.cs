using System;
using System.Threading.Tasks;
using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public interface IUserRepository : ICrudRepository<User, Guid> {
        Task<User> GetUserByCredentials(string username, string password);
    }
}