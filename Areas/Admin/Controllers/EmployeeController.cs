using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simulation_16.Context;
using Simulation_16.Helpers;
using Simulation_16.Models;
using Simulation_16.ViewModels.EmployeeVM;
using System.Threading.Tasks;

namespace Simulation_16.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
    {

        ///GET 
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

        //CREATE

        public IActionResult Create()
        {
            SendBranchesWithViewBag();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateVm vm)
        {

            if (!ModelState.IsValid)
                return View(vm);

            var isExistBranch = _context.Branches.Any(x => x.Id == vm.BranchId);

            if (!isExistBranch)
            {
                ModelState.AddModelError("BranchId", "Branch is not valid.");
                return View(vm);

            }

            if (vm.ImageFile is { })
            {
                if (!vm.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2mb");
                    return View(vm);
                }

                if (!vm.ImageFile.CheckType("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be in image format");
                    return View(vm);
                }

            }
            string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
            string uniquefilename = await vm.ImageFile.SaveFile(folderPath);


            Employee employee = new()
            {
                Name=vm.Name,
                Description=vm.Description,
                Experience=vm.Experience,
                BranchId=vm.BranchId,
                ImageUrl=uniquefilename
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //DELETE
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");

            string imagePath = Path.Combine(folderPath, employee.ImageUrl);


            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            return RedirectToAction(nameof(Index));
        }

        //UPDATE
        public IActionResult Update(int id)
        {
            var employee = _context.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }

            EmployeeUpdateVM vm = new()
            {
                Name = employee.Name,
                Description = employee.Description,
                Experience = employee.Experience,
                BranchId = employee.BranchId,
            };
            SendBranchesWithViewBag();

            return View(vm);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(EmployeeUpdateVM vm)
        {
            SendBranchesWithViewBag();
            if (!ModelState.IsValid)
                return View(vm);

            var existEmployee = await _context.Employees.FindAsync(vm.Id);

            if (existEmployee is null)
            {
                return BadRequest();

            }

            var isExistBranch = _context.Branches.Any(x => x.Id == vm.BranchId);

            if (!isExistBranch)
            {
                ModelState.AddModelError("BranchId", "Branch is not valid.");
                return View(vm);

            }

            if (vm.ImageFile is { })
            {
                if (!vm.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less than 2mb");
                    return View(vm);
                }

                if (!vm.ImageFile.CheckType("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be in image format");
                    return View(vm);
                }

            }
            existEmployee.Name = vm.Name;
            existEmployee.Description = vm.Description;
            existEmployee.Experience = vm.Experience;
            existEmployee.BranchId = vm.BranchId;

            string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");

            if (vm.ImageFile is { })
            {
                string newImagePath = await vm.ImageFile.SaveFile(folderPath);
                string oldImagePath = Path.Combine(folderPath, existEmployee.ImageUrl);
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);

                existEmployee.ImageUrl = newImagePath;
            }

            _context.Employees.Update(existEmployee);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }








        private void SendBranchesWithViewBag()
        {
            var branches = _context.Branches.Select(b => new SelectListItem()
            {
                Text = b.Name,
                Value = b.Id.ToString()
            }).ToString();

            ViewBag.Branches = branches;
        }
    }
}
