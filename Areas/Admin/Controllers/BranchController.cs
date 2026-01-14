using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulation_16.Context;
using Simulation_16.Models;

namespace Simulation_16.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController(AppDbContext _context) : Controller
    {
        //GET
        public IActionResult Index()
        {
            var branches = _context.Branches.Select(b => new Branch()
            {
                Name = b.Name,
                Id = b.Id
            }).ToList();
            return View(branches);
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Branch branch)
        {
            if (!ModelState.IsValid)
                return View(branch);

            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var branch = _context.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            _context.Branches.Remove(branch);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int id)
        {

            var branch = _context.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            Branch br = new()
            {
                Name = branch.Name,
                Id = branch.Id
            };

            branch.Name = br.Name;
            _context.Update(branch);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
