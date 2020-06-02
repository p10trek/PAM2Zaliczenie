using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAM2Zaliczenie.Models
{
    public partial class Users
    {
        public Users()
        {
            Tasks = new HashSet<Tasks>();
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public int UserAccessLevel { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
