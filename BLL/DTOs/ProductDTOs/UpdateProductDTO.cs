namespace VendingMachineAPIs.BLL.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        [MultipleOfCustomized(5, 10, 20, 50, 100)]
        public int Cost { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; } = "";
      
    }
}
