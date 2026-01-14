using System.ComponentModel.DataAnnotations;

namespace Simulation_16.ViewModels.UserVm
{
    public class RegisterVM
    {
        [Required,MaxLength(256)]
        public string Fullname { get; set; }

        [Required,MaxLength(256)]
        public string Username { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string CheckPassword { get; set; }
    }
}
