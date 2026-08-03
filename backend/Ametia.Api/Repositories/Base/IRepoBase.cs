namespace Grad.Repo.Base
{
    public interface IRepoBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAsyncAll();
        Task<T?> GetAsyncByParameter(string str);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int Entity);
        //Task<IEnumerable<T>> TopFive(T Entity);


    }
}
