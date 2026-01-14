using Simulation_16.Models;
using System.ComponentModel.DataAnnotations;

namespace Simulation_16.ViewModels.EmployeeVM
{
    public class EmployeeCreateVm
    {
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; }

        [Required,MaxLength(1024),MinLength(3)]
        public string Description { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required, Range(0,50)]
        public int Experience { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
