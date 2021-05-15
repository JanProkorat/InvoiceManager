using System;
using System.Collections.Generic;

#nullable disable

namespace InvoiceManagerMVC.EFModels
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int XId { get; set; }
        public string InvoiceName { get; set; }
        public int State { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
