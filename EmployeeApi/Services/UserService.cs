using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeApi.Models;
using EmployeeApi.Repository;
using Microsoft.Extensions.Logging;

namespace EmployeeApi.Services
{
    public class UserService : CrudService<User, Guid>, IUserService
    {
        public UserService(
            ILogger<UserService> logger,
            IUserRepository userRepository
        ) : base(logger, userRepository) {

        }
        
        public async Task<User> AuthenticateUser(string username, string password)
        {
            logger.LogInformation($"{username} attempt to login");
            return await ((IUserRepository)repository).GetUserByCredentials(username, password);
        }
    }
}