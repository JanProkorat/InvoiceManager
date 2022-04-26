using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Infrastructure.Handlers;
using InvoiceManagerMVC.Infrastructure.Profiles;
using InvoiceManagerMVC.Infrastructure.Repositories;
using InvoiceManagerMVC.Infrastructure.UnitOfWork;

namespace InvoiceManagerMVC.Handlers.Commands;

public class UpdateInvoiceCommand : ICommand<InvoiceDto>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string InvoiceName { get; set; }
    public InvoiceState State { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }
}

public class UpdateInvoiceCommandHandler : BaseHandler<UpdateInvoiceCommand, InvoiceDto>
{
    private readonly InvoiceRepository _invoiceRepository;
    private readonly InvoiceItemRepository _invoiceItemRepository;

    private Invoice _invoice;
    
    public UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _invoiceRepository = unitOfWork.TypedRepository<InvoiceRepository>();
        _invoiceItemRepository = unitOfWork.TypedRepository<InvoiceItemRepository>();
    }


    public override async Task Validate(UpdateInvoiceCommand command, CancellationToken cancellationToken)
    {
        _invoice = await _invoiceRepository.GetAsync(command.Id);
        if (_invoice == null)
            throw new Exception($"Invoice with ID {command.Id} not found");

        _invoice.InvoiceItems = await _invoiceItemRepository.GetItemsByInvoiceId(command.Id);
    }

    public override async Task<InvoiceDto> Write(UpdateInvoiceCommand command, CancellationToken cancellationToken)
    {
        UnitOfWork.Mapper.Map(command, _invoice);
        _invoiceRepository.Update(_invoice);
        return UnitOfWork.Mapper.Map<InvoiceDto>(_invoice);
    }
}