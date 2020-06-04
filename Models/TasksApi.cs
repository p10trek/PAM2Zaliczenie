using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAM2Zaliczenie.Models
{
    public class TasksApi
    {
        public string TaskName { get; set; }
        public string Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime TimeToEnd { get; set; }
        public bool IsDone { get; set; }
        public bool IsError { get; set; }
        public string Error { get; set; }
    }
}
