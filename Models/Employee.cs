using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<Tasks>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public bool? IsEmployed { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<EmployeesSkils> EmployeesSkils { get; set; }
    }
}
