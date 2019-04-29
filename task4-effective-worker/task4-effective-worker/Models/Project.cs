using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task4_effective_worker.Models {
    public class Project {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int Cost { get; set; }
    }
}