namespace VendingMachineAPIs.DAL.Repo
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity? GetById(string id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
    }
}
