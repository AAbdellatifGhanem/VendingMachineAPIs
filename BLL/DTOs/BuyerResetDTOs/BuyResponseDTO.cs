using VendingMachineAPIs.BLL.DTOs.ProductDTOs;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.BLL.DTOs.BuyerResetDTOs
{
    public class BuyResponseDTO
    {
        public Product? product { get; set; }
        public int Total { get; set; }
        public int? Change {  get; set; }
    }
}
