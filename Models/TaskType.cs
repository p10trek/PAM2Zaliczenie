using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAM2Zaliczenie.Models
{
    public partial class TaskType
    {
        public TaskType()
        {
            Tasks = new HashSet<Tasks>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        [Range(0, 24)]
        public long Duration { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<EmployeesSkils> EmployeesSkils { get; set; }
    }
}
