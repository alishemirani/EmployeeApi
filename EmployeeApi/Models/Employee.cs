using System;

namespace EmployeeApi.Models
{
    public class Employee : Entity<Guid>
    {
        private Guid id;
        public Employee(Guid id) {
            this.id = id;
        }

        public override Guid Id {
            get { return id; }
        }
        public Guid UserId {get;set;}
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string SkillSets { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public bool IsActive {get;set;}
    }
}