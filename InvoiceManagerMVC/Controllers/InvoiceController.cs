using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.Handlers.Commands;
using InvoiceManagerMVC.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace InvoiceManagerMVC.Controllers
{
    // [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<InvoiceDto>>> GetInvoiceList()
        {
            return Ok(await _mediator.Send(new GetInvoiceListQuery()));
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("{invoiceId:int}")]
        public async Task<ActionResult<InvoiceDto>> UpdateInvoice([FromRoute] int invoiceId, [FromBody] UpdateInvoiceCommand command)
        {
            command.SetId(invoiceId);
            return Ok(await _mediator.Send(command));
        }
    }
}
