using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachineAPIs.BLL.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        [MultipleOfCustomized(5, 10, 20, 50, 100)]
        public int Cost { get; set; }
        [Required]
        public int Amount { get; set; }

    }
}
