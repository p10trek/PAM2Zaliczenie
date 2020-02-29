using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public bool? IsEmployed { get; set; }
    }
}
