using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagerMVC.Models.Managers;
using InvoiceManagerMVC.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InvoiceManagerMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        public readonly IInvoiceManager _invoiceManager;

        public InvoiceController(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<InvoiceDTO>> GetInvoiceList()
        {
            return _invoiceManager.GetUnpaidInvoices();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<InvoiceDTO> CreateInvoice([FromBody] InvoiceDTO invoice)
        {
            return _invoiceManager.CreateInvoice(invoice);
        }

        [HttpPatch]
        [Route("{invoiceId}")]
        public ActionResult<InvoiceDTO> UpdateInvoice([FromRoute] int invoiceId, [FromBody] UpdateInvoiceRequest request)
        {
            var result = _invoiceManager.UpdateInvoice(invoiceId, request.InvoiceName);
            return result == null ? NotFound() : result;
        }

        [HttpPost]
        [Route("add/item")]
        public ActionResult<InvoiceItemDTO> CreateInvoiceItem([FromBody] InvoiceItemDTO invoiceItem)
        {
            return _invoiceManager.CreateInvoiceItem(invoiceItem);
        }

        [HttpDelete]
        [Route("delete/item/{itemId}")]
        public ActionResult DeleteInvoiceItem([FromRoute] int itemId)
        {
            var result = _invoiceManager.DeleteInvoiceItem(itemId);
            return result ? Ok() : NotFound();
        }

        [HttpPost]
        [Route("{invoiceId}")]
        public ActionResult PayInvoice([FromRoute] int invoiceId)
        {
            var result = _invoiceManager.PayInvoice(invoiceId);
            return result ? Ok() : NotFound();
        }
    }
}
