using System.Threading.Tasks;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagerMVC.Controllers;

[Route("api/invoice")]
[ApiController]
public class InvoiceItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceItemController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("{id:int}/item")]
    public async Task<ActionResult<InvoiceItemDto>> CreateInvoiceItem([FromRoute] int id, [FromBody] CreateItemForInvoiceCommand command)
    {
        command.SetInvoiceId(id);
        return Ok(await _mediator.Send(command));
    }
    
    [HttpDelete]
    [Route("item/{id:int}")]
    public async Task<ActionResult> DeleteInvoiceItem([FromRoute] int id)
    {
        return Ok(await _mediator.Send(new DeleteInvoiceItemCommand(id)));
    }
}