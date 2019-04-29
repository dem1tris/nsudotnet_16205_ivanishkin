using Microsoft.EntityFrameworkCore;

namespace task4_effective_worker.Models {
    public class WorkingContext : DbContext {
//        public WorkingContext(DbContextOptions<WorkingContext> options)
        public WorkingContext(DbContextOptions options)
            : base(options) {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}