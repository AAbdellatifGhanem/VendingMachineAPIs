using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.DAL.Repo
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {

        private readonly AppDbContext _appDbContext;

        public ProductRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
