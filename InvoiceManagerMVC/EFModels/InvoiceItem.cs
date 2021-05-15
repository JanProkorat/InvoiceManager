using System;
using System.Collections.Generic;

#nullable disable

namespace InvoiceManagerMVC.EFModels
{
    public partial class InvoiceItem
    {
        public int XId { get; set; }
        public int AmountToPay { get; set; }
        public string Receiver { get; set; }
        public DateTime? Deadline { get; set; }
        public int KInvoice { get; set; }

        public virtual Invoice KInvoiceNavigation { get; set; }
    }
}
