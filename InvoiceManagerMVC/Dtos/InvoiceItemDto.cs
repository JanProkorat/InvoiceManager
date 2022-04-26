using System;

namespace InvoiceManagerMVC.Dtos;

public class InvoiceItemDto
{
    public int Id { get; set; }
    public int AmountToPay { get; set; }
    public string Receiver { get; set; }
    public DateTime? Deadline { get; set; }
    public int KInvoice { get; set; }
}