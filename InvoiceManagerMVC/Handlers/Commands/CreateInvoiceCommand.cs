using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Infrastructure.Handlers;
using InvoiceManagerMVC.Infrastructure.Profiles;
using InvoiceManagerMVC.Infrastructure.Repositories;
using InvoiceManagerMVC.Infrastructure.UnitOfWork;

namespace InvoiceManagerMVC.Handlers.Commands;

public class CreateInvoiceCommand : ICommand<InvoiceDto>
{
    public string InvoiceName { get; set; }
    public InvoiceState State { get; set; }
    public List<CreateInvoiceItemCommand> Items { get; set; }
}

public class CreateInvoiceItemCommand
{
    public int AmountToPay { get; set; }
    public string Receiver { get; set; }
    public DateTime? Deadline { get; set; }
}

public class CreateInvoiceCommandHandler : BaseHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly InvoiceRepository _invoiceRepository;

    public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _invoiceRepository = unitOfWork.TypedRepository<InvoiceRepository>();
    }

    public override Task Validate(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        if (command.Items.Any(x => x.Deadline.HasValue && x.Deadline < DateTime.Today))
            throw new Exception("Deadline cannot be in the past.");
        
        return Task.CompletedTask;
    }

    public override async Task<InvoiceDto> Write(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        var invoice = UnitOfWork.Mapper.Map<Invoice>(command);
        var addedEntity = await _invoiceRepository.InsertAsync(invoice);
        await UnitOfWork.SaveChangesAsync();

        return UnitOfWork.Mapper.Map<InvoiceDto>(addedEntity);
    }
}