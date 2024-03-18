namespace VendingMachineAPIs.BLL.DTOs
{
    public class UpdateUserDTO
    {
        public string? UserName { get; set; }
        [MultipleOfCustomized(5, 10, 20, 50, 100)]
        public int? deposit {  get; set; }
    }
}
