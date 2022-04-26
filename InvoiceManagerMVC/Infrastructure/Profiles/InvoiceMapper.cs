using AutoMapper;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Handlers.Commands;

namespace InvoiceManagerMVC.Infrastructure.Profiles;

public enum InvoiceState
{
    Unpaid,
    Paid
}

public class InvoiceMapper : Profile
{
    public InvoiceMapper()
    {
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(r => r.Id, o => o.MapFrom(s => s.XId))
            .ForMember(r => r.InvoiceName, o => o.MapFrom(s => s.InvoiceName))
            .ForMember(r => r.State, o => o.MapFrom(s => s.State.ToString()))
            .ForMember(r => r.Items, o => o.MapFrom(s => s.InvoiceItems));
        
        CreateMap<InvoiceDto, Invoice>()
            .ForMember(r => r.XId, o => o.MapFrom(s => s.Id))
            .ForMember(r => r.InvoiceName, o => o.MapFrom(s => s.InvoiceName))
            .ForMember(r => r.State, o => o.MapFrom(s => s.State))
            .ForMember(r => r.InvoiceItems, o => o.MapFrom(s => s.Items));
        
         CreateMap<CreateInvoiceCommand, Invoice>()
            .ForMember(r => r.XId, o => o.Ignore())
            .ForMember(r => r.InvoiceName, o => o.MapFrom(s => s.InvoiceName))
            .ForMember(r => r.State, o => o.MapFrom(s => s.State))
            .ForMember(r => r.InvoiceItems, o => o.MapFrom(s => s.Items));
        
        CreateMap<UpdateInvoiceCommand, Invoice>()
            .ForMember(r => r.XId, o => o.MapFrom(s => s.Id))
            .ForMember(r => r.InvoiceName, o => o.MapFrom(s => s.InvoiceName))
            .ForMember(r => r.State, o => o.MapFrom(s => s.State))
            .ForMember(r => r.InvoiceItems, o => o.Ignore());
        
        
    }
}