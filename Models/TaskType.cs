using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class TaskType
    {
        public TaskType()
        {
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public long Duration { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
