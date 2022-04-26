using InvoiceManagerMVC.EFModels;

namespace InvoiceManagerMVC.Infrastructure.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public InvoiceRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}