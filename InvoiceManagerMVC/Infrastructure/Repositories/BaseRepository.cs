using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManagerMVC.EFModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagerMVC.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly InvoiceDbContext Context;

        protected BaseRepository(InvoiceDbContext context)
        {
            Context = context;
        }

        public async Task<List<TEntity>> GetListAsync(string includes)
        {
            return await Context.Set<TEntity>().Include(includes).ToListAsync();
        }
        
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityAdded = await Context.AddAsync(entity);

            return entityAdded.Entity;
        }
        
        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}