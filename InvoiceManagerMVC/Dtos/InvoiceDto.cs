using System.Collections.Generic;
using InvoiceManagerMVC.Infrastructure.Profiles;

namespace InvoiceManagerMVC.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceName { get; set; }
        public string State { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }
}