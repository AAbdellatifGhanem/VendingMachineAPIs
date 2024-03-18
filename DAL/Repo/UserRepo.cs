using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.DAL.Repo
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
       
        private readonly AppDbContext _appDbContext;

        public UserRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
