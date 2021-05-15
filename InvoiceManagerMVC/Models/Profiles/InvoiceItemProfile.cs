using System;
using AutoMapper;
using InvoiceManagerMVC.EFModels;

namespace InvoiceManagerMVC.Models.Profiles
{

    public class InvoiceItemDTO
    {
        public int Id { get; set; }
        public int AmountToPay { get; set; }
        public string Receiver { get; set; }
        public DateTime? Deadline { get; set; }
        public int KInvoice { get; set; }
    }

    public class InvoiceItemProfile: Profile
    {
        public InvoiceItemProfile()
        {
            CreateMap<InvoiceItem, InvoiceItemDTO>()
                .ForMember(r => r.Id, o => o.MapFrom(s => s.XId))
                .ForMember(r => r.AmountToPay, o => o.MapFrom(s => s.AmountToPay))
                .ForMember(r => r.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(r => r.Receiver, o => o.MapFrom(s => s.Receiver))
                .ForMember(r => r.KInvoice, o => o.MapFrom(s => s.KInvoice));
        }
    }

    public class InvoiceItemEFProfile : Profile
    {
        public InvoiceItemEFProfile()
        {
            CreateMap<InvoiceItemDTO, InvoiceItem>()
                .ForMember(r => r.XId, o => o.MapFrom(s => s.Id))
                .ForMember(r => r.AmountToPay, o => o.MapFrom(s => s.AmountToPay))
                .ForMember(r => r.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(r => r.Receiver, o => o.MapFrom(s => s.Receiver))
                .ForMember(r => r.KInvoice, o => o.MapFrom(s => s.KInvoice));
        }
    }
}
