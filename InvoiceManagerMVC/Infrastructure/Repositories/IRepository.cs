using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagerMVC.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetListAsync(string includes);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> InsertAsync(TEntity entity);
        void Update(TEntity entity);

    }
}