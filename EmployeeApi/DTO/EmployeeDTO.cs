using System;

namespace EmployeeApi.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public Guid UserId {get;set;}
        public string Role { get; set; }
        public string Department { get; set; }
        public string SkillSets { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public bool IsActive {get;set;}
    }

    public class FullEmployeeDTO : EmployeeDTO {
        public Guid Id { get; set; }
    }
}