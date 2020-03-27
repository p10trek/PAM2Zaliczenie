using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class EmployeesSkils
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TaskTypeId { get; set; }
        public string ExperienceDescription { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TaskType TaskType { get; set; }
    }
}
