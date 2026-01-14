using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Simulation_16.Models;

namespace Simulation_16.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
