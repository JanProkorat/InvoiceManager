using System.Threading.Tasks;
using AutoMapper;
using InvoiceManagerMVC.EFModels;

namespace InvoiceManagerMVC.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        TRepo TypedRepository<TRepo>() where TRepo : class;
        InvoiceDbContext Context { get; }
        IMapper Mapper { get; }
        Task SaveChangesAsync();
    }
}