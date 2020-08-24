using System;
using System.Collections.Generic;

namespace EmployeeApi.Models
{
    public class User : Entity<Guid>
    {
        private Guid id;

        public User(Guid id) {
            this.id = id;
        }

        public override Guid Id => id;
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }

        public bool HasRole(string role) {
            return Roles.Contains(role);
        }
    }
}