using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class TaskType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Duration { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
    }
}
