using Microsoft.AspNetCore.Identity;

namespace VendingMachineAPIs.DAL.Models
{
    public class User : IdentityUser
    {
        public int? deposit { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
