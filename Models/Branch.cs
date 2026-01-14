using Simulation_16.Models.Common;

namespace Simulation_16.Models
{
    public class Branch:BaseEntity
    {
        public  string Name { get; set; }

        public List<Employee> Employees { get; set; }

    }
}
