using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Infrastructure.Handlers;
using InvoiceManagerMVC.Infrastructure.Repositories;
using InvoiceManagerMVC.Infrastructure.UnitOfWork;

namespace InvoiceManagerMVC.Handlers.Commands;

public class CreateItemForInvoiceCommand : ICommand<InvoiceItemDto>
{
    [JsonIgnore]
    public int InvoiceId { get; set; }
    public int AmountToPay { get; set; }
    public string Receiver { get; set; }
    public DateTime? Deadline { get; set; }

    public void SetInvoiceId(int id)
    {
        InvoiceId = id;
    }
}

public class CreateItemForInvoiceCommandHandler : BaseHandler<CreateItemForInvoiceCommand, InvoiceItemDto>
{
    
    private readonly InvoiceRepository _invoiceRepository;
    private readonly InvoiceItemRepository _invoiceItemRepository;
    
    public CreateItemForInvoiceCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _invoiceRepository = unitOfWork.TypedRepository<InvoiceRepository>();
        _invoiceItemRepository = unitOfWork.TypedRepository<InvoiceItemRepository>();
    }

    public override async Task Validate(CreateItemForInvoiceCommand command, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetAsync(command.InvoiceId);
        if (invoice == null)
            throw new Exception($"Invoice with ID {command.InvoiceId} not found.");
    }

    public override async Task<InvoiceItemDto> Write(CreateItemForInvoiceCommand command, CancellationToken cancellationToken)
    {
        var item = UnitOfWork.Mapper.Map<InvoiceItem>(command);
        var addedItem = await _invoiceItemRepository.InsertAsync(item);
        await UnitOfWork.SaveChangesAsync();
        return UnitOfWork.Mapper.Map<InvoiceItemDto>(addedItem);
    }
}