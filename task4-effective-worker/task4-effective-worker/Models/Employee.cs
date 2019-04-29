using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task4_effective_worker.Models {
    public class Employee {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}