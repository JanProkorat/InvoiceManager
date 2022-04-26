using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagerMVC.EFModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagerMVC.Infrastructure.Repositories
{
    public class InvoiceItemRepository : BaseRepository<InvoiceItem>
    {
        public InvoiceItemRepository(InvoiceDbContext context) : base(context)
        {
        }

        public async Task<ICollection<InvoiceItem>> GetItemsByInvoiceId(int invoiceId)
        {
            return await Context.InvoiceItems.Where(x => x.KInvoice == invoiceId).ToListAsync();
        }

        public void RemoveInvoiceItem(InvoiceItem item)
        {
            Context.InvoiceItems.Remove(item);
        }
    }
}