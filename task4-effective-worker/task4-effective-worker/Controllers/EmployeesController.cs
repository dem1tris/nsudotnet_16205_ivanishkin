using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using task4_effective_worker.Models;

namespace task4_effective_worker.Controllers {
    public class EmployeesController : Controller {
        private readonly WorkingContext _context;

        public EmployeesController(WorkingContext context) {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index() {
            var list = await _context.Employees.Include(e => e.Projects).Select(e => new EmployeeWrapper(e) {
                Salary = e.Projects.Sum(p => p.Cost)
            }).ToListAsync();

            var sorted = list.OrderByDescending(e => e.Salary).ToList();

            return View(sorted);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,Patronymic,LastName")]
            Employee employee) {
            if (ModelState.IsValid) {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null) {
                return NotFound();
            }

            await _context.Entry(employee).Collection(e => e.Projects).LoadAsync();

            ViewBag.UnassignedProjects = new SelectList(
                _context.Projects.Where(p => p.EmployeeId == null), "ProjectId", "Title"
            );

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,Patronymic,LastName")]
            Employee employee) {
            if (id != employee.EmployeeId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!EmployeeExists(employee.EmployeeId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("UnassignProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnassignProject(int employeeId, int projectId) {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null) {
                project.EmployeeId = null;
                _context.Update(project);
                await _context.SaveChangesAsync();
            }

            return Redirect($"Edit/{employeeId}");
        }

        [HttpPost, ActionName("AssignProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignProject(int employeeId, int projectId) {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null) {
                project.EmployeeId = employeeId;
                _context.Update(project);
                await _context.SaveChangesAsync();
            }

            return Redirect($"Edit/{employeeId}");
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            using (var transaction = _context.Database.BeginTransaction()) {
                var employee = await _context.Employees.FindAsync(id);
                await _context.Entry(employee).Collection(e => e.Projects).LoadAsync();
                var projectIds = employee.Projects.Select(p => p.ProjectId).ToList();
                foreach (var projectId in projectIds) {
                    await UnassignProject(id, projectId);
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id) {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}