using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachineAPIs.BLL.DTOs.ProductDTOs
{
    public class GetAllProductsDTO
    {
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Cost { get; set; }
    }
}
