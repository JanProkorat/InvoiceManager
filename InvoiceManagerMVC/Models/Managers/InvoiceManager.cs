using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Models.Profiles;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagerMVC.Models.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        public readonly DB_DevelopContext _ctx;
        private readonly IMapper _mapper;

        public InvoiceManager(DB_DevelopContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

       
        public List<InvoiceDTO> GetUnpaidInvoices()
        {
            var invoices = _ctx.Invoices.Where(x => x.State == (int)InvoiceState.nezaplaceno).Include(x => x.InvoiceItems).ToList();
            return _mapper.Map<List<InvoiceDTO>>(invoices);

        }

        public InvoiceDTO CreateInvoice(InvoiceDTO invoice)
        {
            var invoiceDB = _mapper.Map<Invoice>(invoice);
            _ctx.Invoices.Add(invoiceDB);
            _ctx.SaveChanges();
            return _mapper.Map<InvoiceDTO>(invoiceDB);
        }

        public InvoiceDTO UpdateInvoice(int invoiceId, string invoiceName)
        {
            var invoiceDB = _ctx.Invoices.Where(x => x.XId == invoiceId).Include(x => x.InvoiceItems).SingleOrDefault();
            if(invoiceDB == null)
            {
                return null;
            }
            else
            {
                invoiceDB.InvoiceName = invoiceName;
                _ctx.SaveChanges();
                return _mapper.Map<InvoiceDTO>(invoiceDB);
            }

        }

        public InvoiceItemDTO CreateInvoiceItem(InvoiceItemDTO item)
        {
            var itemDB = _mapper.Map<InvoiceItem>(item);
            _ctx.InvoiceItems.Add(itemDB);
            _ctx.SaveChanges();
            return _mapper.Map<InvoiceItemDTO>(itemDB);
        }

        public bool DeleteInvoiceItem(int itemId)
        {
            try
            {
                var item = _ctx.InvoiceItems.Single(x => x.XId == itemId);
                _ctx.Remove(item);
                _ctx.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool PayInvoice(int invoiceId)
        {
            try
            {
                var item = _ctx.Invoices.Single(x => x.XId == invoiceId);
                item.State = (int)InvoiceState.zaplaceno;
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
