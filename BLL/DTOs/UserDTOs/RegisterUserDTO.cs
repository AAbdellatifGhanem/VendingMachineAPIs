using System.ComponentModel.DataAnnotations;

namespace VendingMachineAPIs.BLL.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
        [MultipleOfCustomized(5, 10, 20, 50, 100)]
        public int? deposite {  get; set; }
    }
}
