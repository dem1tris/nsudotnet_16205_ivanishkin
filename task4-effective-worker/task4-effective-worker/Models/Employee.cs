using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task4_effective_worker.Models {
    public class Employee {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }
        public int? Cabinet { get; set; }

        public string FullName => $"{FirstName} {Patronymic} {LastName}";

        public ICollection<Project> Projects { get; set; }
    }
}