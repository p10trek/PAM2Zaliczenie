using System;

namespace PAM2Zaliczenie.Models
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public int TaskTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public bool IsReady { get; set; }
        public DateTime StartTime { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual Users User { get; set; }
    }
}
