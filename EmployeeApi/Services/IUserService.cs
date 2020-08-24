using System;
using System.Threading.Tasks;
using EmployeeApi.Models;

namespace EmployeeApi.Services
{
    public interface IUserService : ICrudService<User, Guid>
    {
        Task<User> AuthenticateUser(string username, string password);
    }
}
