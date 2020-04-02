using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeesSkils = new HashSet<EmployeesSkils>();
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        //todo: dolaczyc fotki jesli starczy czasu
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public bool? IsEmployed { get; set; }

        public virtual ICollection<EmployeesSkils> EmployeesSkils { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
