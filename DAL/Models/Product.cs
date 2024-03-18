using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachineAPIs.DAL.Models
{
    public class Product
    {
        [Key]

        public string ProductId { get; set; }
      
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string? ProductName { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }



    }
}
