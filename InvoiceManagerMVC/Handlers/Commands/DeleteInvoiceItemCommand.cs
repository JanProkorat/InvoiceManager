using System;
using System.Threading;
using System.Threading.Tasks;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Infrastructure.Handlers;
using InvoiceManagerMVC.Infrastructure.Repositories;
using InvoiceManagerMVC.Infrastructure.UnitOfWork;

namespace InvoiceManagerMVC.Handlers.Commands;

public class DeleteInvoiceItemCommand : ICommand<Task>
{
    public DeleteInvoiceItemCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}


public class DeleteInvoiceItemCommandHandler : BaseHandler<DeleteInvoiceItemCommand, Task>
{
    
    private readonly InvoiceItemRepository _invoiceItemRepository;

    private InvoiceItem _invoiceItem;
    
    public DeleteInvoiceItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _invoiceItemRepository = unitOfWork.TypedRepository<InvoiceItemRepository>();
    }

    public override async Task Validate(DeleteInvoiceItemCommand command, CancellationToken cancellationToken)
    {
        _invoiceItem = await _invoiceItemRepository.GetAsync(command.Id);
        if(_invoiceItem == null)
            throw new Exception($"Invoice item with ID {command.Id} not found.");

    }

    public override async Task<Task> Write(DeleteInvoiceItemCommand command, CancellationToken cancellationToken)
    {
        _invoiceItemRepository.RemoveInvoiceItem(_invoiceItem);
        await UnitOfWork.SaveChangesAsync();
        
        return Task.CompletedTask;
    }
}