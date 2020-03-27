using System;
using System.Collections.Generic;

namespace PAM2Zaliczenie.Models
{
    public partial class TaskType
    {
        public TaskType()
        {
            EmployeesSkils = new HashSet<EmployeesSkils>();
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        //todo: dorobić logike obliczania daty zakonczenia zlecenia, duration wyrazamy w godzinach
        //todo: dodanie wyboru waluty i pobieranie kursy z api
        //todo: wystawienie Api czy wszystko jest skonczone ew. lista zadan w trakcie
        public long Duration { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<EmployeesSkils> EmployeesSkils { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
