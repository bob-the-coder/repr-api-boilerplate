namespace DataAccessExtensions.Contract;

public interface IRepository
{
}

public interface IRepository<TEntity> : IRepository where TEntity : IEntity
{
    TEntity? Create(TEntity? entity);
    TEntity? Update(TEntity? entity);
    bool Delete(TEntity? entity);
}

public interface IPkRepository<TEntity, in TPk> : IRepository<TEntity> 
    where TEntity : IPkEntity<TPk>
    where TPk : IComparable
{
    TEntity? Get(TPk key);
    bool Delete(TPk key);
}