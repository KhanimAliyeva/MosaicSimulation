using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation_16.Context;
using Simulation_16.Models;
using Simulation_16.ViewModels.EmployeeVM;

namespace Simulation_16.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
      

        public async Task<IActionResult> IndexAsync()
        {
            var employees = await _context.Employees.Select(employee => new EmployeeGetVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Description = employee.Description,
                Experience = employee.Experience,
                BranchName = employee.Branch.Name,
                ImageUrl = employee.ImageUrl
            }).ToListAsync();
            return View(employees);
        }

        
    }
}
