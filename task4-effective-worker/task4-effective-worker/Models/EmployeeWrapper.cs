using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task4_effective_worker.Models {
    public class EmployeeWrapper : Employee {

        public EmployeeWrapper(Employee e) {
            EmployeeId = e.EmployeeId;
            FirstName = e.FirstName;
            Patronymic = e.Patronymic;
            LastName = e.LastName;
            Projects = e.Projects;
        }

        public int Salary { get; set; }

        public new string FullName => $"{FirstName} {Patronymic} {LastName}";
    }
}