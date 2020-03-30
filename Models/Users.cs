﻿using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class Users
    {
        public Users()
        {
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        private string _password;
        public string Password { get { return _password; } set { _password = passwordSHA512(value); } }
        public string emailAddress { get; set; }
        public int UserAccessLevel { get; set; }

        //todo : obsluga ciasteczek

        public virtual ICollection<Tasks> Tasks { get; set; }

        //password hash SHA512 
        private string passwordSHA512(string inputPassword)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(inputPassword);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
