using System.ComponentModel.DataAnnotations;

namespace VendingMachineAPIs.BLL.DTOs.UserDTOs
{
    public class LoginDTOModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
