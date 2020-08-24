using System;
using System.Collections.Generic;

namespace EmployeeApi.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }

    public class FullUserDTO : UserDTO {
        public Guid Id { get; set; }
    }
}