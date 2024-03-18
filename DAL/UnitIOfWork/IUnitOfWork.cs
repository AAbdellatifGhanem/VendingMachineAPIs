using VendingMachineAPIs.DAL.Repo;

namespace VendingMachineAPIs.DAL.UnitIOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepo _userRepo { get; }
        public IProductRepo _productRepo { get; }
        int SaveChanges(); 
    }
}
