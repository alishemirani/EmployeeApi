using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static List<User> users = new List<User>() {
            new User(Guid.Parse("ff9df80c-cfd2-4cd1-804a-f410ec301966")) {  UserName = "admin", Password = "password", FullName = "Ali Shemirani", Roles = new List<string>() {"Admin"} },
        };

        public Task<bool> Delete(User entity)
        {
            var index = users.FindIndex(t => t.Id == entity.Id);
            if (index < 0)
            {
                return Task.FromResult(false);
            }
            users.RemoveAt(index);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return Task.FromResult(users.AsEnumerable());
        }

        public Task<User> GetById(Guid id)
        {
            return Task.FromResult(
                users.Where(t => t.Id == id)
                    .FirstOrDefault()
            );
        }

        public Task<User> GetUserByCredentials(string username, string password)
        {
            return Task.FromResult(
                users.Where(t => t.UserName == username && t.Password == password)
                .FirstOrDefault()
            );
        }

        public Task<User> Insert(User entity)
        {
            users.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> Update(User entity)
        {
            var index = users.FindIndex(t => t.Id == entity.Id);
            if (index < 0)
            {
                return Task.FromResult(false);
            }
            users[index] = entity;
            return Task.FromResult(true);

        }
    }
}