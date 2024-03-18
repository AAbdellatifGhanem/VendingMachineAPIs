namespace VendingMachineAPIs.BLL.DTOs
{
    public class GetUserResponseDTO
    {
        public string Token { get; set; } 
        public DateTime ExpireDate { get; set; }
        public string UserName { get; set; }    
        public string UserId { get; set; }
    }
}
