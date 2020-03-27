using System;
using System.Collections.Generic;

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
        private string password;
        //todo : dodać pole email
        //todo : obsluga ciasteczek
        //todo : dodanie pola w tabeli ktore definiowalo by poziom uprawnien uzytkownik, administrator
        public string Password { get { return password; } set { password = value; } }

        public virtual ICollection<Tasks> Tasks { get; set; }
        //public int Privilages{get ;set };
    }
}
