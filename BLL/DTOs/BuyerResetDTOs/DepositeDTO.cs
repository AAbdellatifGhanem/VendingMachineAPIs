using System.ComponentModel.DataAnnotations;

namespace VendingMachineAPIs.BLL.DTOs.BuyerResetDTOs
{
    public class DepositeDTO
    {
        [MultipleOfCustomized(5, 10, 20, 50, 100)]
        public int added_deposit {  get; set; }
    }
}
