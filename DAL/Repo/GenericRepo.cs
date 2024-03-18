namespace VendingMachineAPIs.DAL.Repo
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
       
        private readonly AppDbContext _appDbContext;

        public GenericRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
   

        public List<TEntity> GetAll() => _appDbContext.Set<TEntity>().ToList();

        public TEntity? GetById(string id) => _appDbContext.Set<TEntity>().Find(id);

        public TEntity Add(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            _appDbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Update(entity);
            _appDbContext.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
            _appDbContext.SaveChanges();
            return entity;
        }


    }
}
