using System;

namespace PAM2Zaliczenie.Models
{
    public partial class EmployeesSkils
    {
        public Guid id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid TaskTypeId { get; set; }
        public string ExperienceDescription { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TaskType TaskType { get; set; }
    }
}
