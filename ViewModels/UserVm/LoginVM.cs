using System.ComponentModel.DataAnnotations;

namespace Simulation_16.ViewModels.UserVm
{
    public class LoginVM
    {
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required,DataType(nameof(Password))]
        public string Password{ get; set; }

        public bool IsRemember { get; set; }
    }
}
