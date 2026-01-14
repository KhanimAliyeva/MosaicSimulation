using Simulation_16.Models;

namespace Simulation_16.ViewModels.EmployeeVM
{
    public class EmployeeGetVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string BranchName{ get; set; }

        public int Experience { get; set; }

        public string ImageUrl { get; set; }
    }
}
