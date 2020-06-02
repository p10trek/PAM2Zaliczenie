using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class Tasks
    {
        public Guid Id { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid UserId { get; set; }
        public bool? IsReady { get; set; }
        public DateTime StartTime { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual Users User { get; set; }

    }
}
