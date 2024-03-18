using VendingMachineAPIs.DAL.Repo;

namespace VendingMachineAPIs.DAL.UnitIOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        
        public UnitOfWork(AppDbContext appDbContext,IUserRepo userRepo, IProductRepo productRepo )
        {
            _appDbContext = appDbContext;
            _userRepo = userRepo;
            _productRepo = productRepo;
        }
        public IUserRepo _userRepo
        {
            get;
            private set;
        }
        public IProductRepo _productRepo
        {
            get;
            private set;
        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }
        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

    }
}
