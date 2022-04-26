using AutoMapper;
using InvoiceManagerMVC.Dtos;
using InvoiceManagerMVC.EFModels;
using InvoiceManagerMVC.Handlers.Commands;

namespace InvoiceManagerMVC.Infrastructure.Profiles
{
    public class InvoiceItemMapper: Profile
    {
        public InvoiceItemMapper()
        {
            CreateMap<InvoiceItem, InvoiceItemDto>()
                .ForMember(r => r.Id, o => o.MapFrom(s => s.XId))
                .ForMember(r => r.AmountToPay, o => o.MapFrom(s => s.AmountToPay))
                .ForMember(r => r.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(r => r.Receiver, o => o.MapFrom(s => s.Receiver))
                .ForMember(r => r.KInvoice, o => o.MapFrom(s => s.KInvoice));
            
            CreateMap<InvoiceItemDto, InvoiceItem>()
                .ForMember(r => r.XId, o => o.MapFrom(s => s.Id))
                .ForMember(r => r.AmountToPay, o => o.MapFrom(s => s.AmountToPay))
                .ForMember(r => r.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(r => r.Receiver, o => o.MapFrom(s => s.Receiver))
                .ForMember(r => r.KInvoice, o => o.MapFrom(s => s.KInvoice));
            
            CreateMap<CreateInvoiceItemCommand, InvoiceItem>()
                .ForMember(r => r.XId, o => o.Ignore())
                .ForMember(r => r.AmountToPay, o => o.MapFrom(s => s.AmountToPay))
                .ForMember(r => r.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(r => r.Receiver, o => o.MapFrom(s => s.Receiver))
                .ForMember(r => r.KInvoice, o => o.Ignore());
            
            CreateMap<CreateItemForInvoiceCommand, InvoiceItem>()
                .ForMember(r => r.XId, o => o.Ignore())
                .ForMember(r => r.AmountToPay, o => o.MapFrom(s => s.AmountToPay))
                .ForMember(r => r.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(r => r.Receiver, o => o.MapFrom(s => s.Receiver))
                .ForMember(r => r.KInvoice, o => o.MapFrom(s => s.InvoiceId));
            
            
        }
    }
}
