using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Infrastructure.Handlers;
using InvoiceManagerMVC.Infrastructure.Repositories;
using InvoiceManagerMVC.Infrastructure.UnitOfWork;

namespace InvoiceManagerMVC.Handlers.Queries
{
    public class GetInvoiceListQuery : IQuery<List<InvoiceDto>>
    {
    }

    public class GetInvoiceListQueryHandler : BaseHandler<GetInvoiceListQuery, List<InvoiceDto>>
    {
        private readonly InvoiceRepository _invoiceRepository;
        
        public GetInvoiceListQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _invoiceRepository = unitOfWork.TypedRepository<InvoiceRepository>();
        }

        public override Task Validate(GetInvoiceListQuery command, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override async Task<List<InvoiceDto>> Write(GetInvoiceListQuery command, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetListAsync("InvoiceItems");
            return UnitOfWork.Mapper.Map<List<InvoiceDto>>(data);
        }
    }
}