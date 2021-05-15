using System;
using System.Collections.Generic;
using InvoiceManagerMVC.Models.Profiles;

namespace InvoiceManagerMVC.Models.Managers
{
    public interface IInvoiceManager
    {

        /// <summary>
        /// vrátí seznam všech faktur
        /// </summary>
        /// <returns>list faktur</returns>
        public List<InvoiceDTO> GetUnpaidInvoices();

        /// <summary>
        /// vytvoří novou fakturu
        /// </summary>
        /// <param name="invoice">faktura</param>
        /// <returns>nově vytvořená faktura</returns>
        public InvoiceDTO CreateInvoice(InvoiceDTO invoice);

        /// <summary>
        /// upraví existující fakturu 
        /// </summary>
        /// <param name="invoiceId">ID faktury</param>
        /// <param name="invoiceName">nový název faktury</param>
        /// <returns>upravená faktura</returns>
        public InvoiceDTO UpdateInvoice(int invoiceId, string invoiceName);

        /// <summary>
        /// vytvoří novou položku faktury
        /// </summary>
        /// <param name="item">položka faktury</param>
        /// <returns>nová položka faktury</returns>
        public InvoiceItemDTO CreateInvoiceItem(InvoiceItemDTO item);

        /// <summary>
        /// odstraní položku faktury
        /// </summary>
        /// <param name="itemId">ID položky faktury</param>
        /// <returns>true - vše OK, false - nepovedlo se odtranit</returns>
        public bool DeleteInvoiceItem(int itemId);

        /// <summary>
        /// zaplatí fakturu
        /// </summary>
        /// <param name="invoiceId"> ID existující faktury</param>
        /// <returns>true - vše OK, false - nepovedlo se zaplatit</returns>
        bool PayInvoice(int invoiceId);
    }
}
