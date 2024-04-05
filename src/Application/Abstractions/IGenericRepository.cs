namespace Application.Abstractions
{
    public interface IGenericRepository<TEntity>
    {
        public ValueTask<TEntity> CreateAsync(TEntity entity);
        public IQueryable<TEntity> GetAllAsync();
        public ValueTask<TEntity> GetByIdAsync(int id);
        public ValueTask<TEntity> UpdateAsync(TEntity entity);
        public ValueTask<TEntity> DeleteAsync(int Id);
    }
}
