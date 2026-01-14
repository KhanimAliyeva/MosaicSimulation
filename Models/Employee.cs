using Simulation_16.Models.Common;

namespace Simulation_16.Models
{
    public class Employee:BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public Branch Branch { get; set; }

        public int BranchId { get; set; }

        public int Experience { get; set; }

        public string ImageUrl { get; set; }

    }
}
