using System;
using System.Collections.Generic;
using AutoMapper;
using InvoiceManagerMVC.EFModels;

namespace InvoiceManagerMVC.Models.Profiles
{

    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string InvoiceName { get; set; }
        public InvoiceState State { get; set; }
        public List<InvoiceItemDTO> Items { get; set; }
    }

    public enum InvoiceState
    {
        nezaplaceno, zaplaceno
    }

    public class UpdateInvoiceRequest
    {
        public string InvoiceName { get; set; }
    }

    public class InvoiceProfile: Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(r =>r.Id, o => o.MapFrom(s => s.XId))
                .ForMember(r => r.InvoiceName, o => o.MapFrom(s => s.InvoiceName))
                .ForMember(r => r.State, o => o.MapFrom(s => s.State))
                .ForMember(r => r.Items, o => o.MapFrom(s => s.InvoiceItems));
        }
    }

    public class InvoiceEFProfile : Profile
    {
        public InvoiceEFProfile()
        {
            CreateMap<InvoiceDTO, Invoice>()
                .ForMember(r => r.XId, o => o.MapFrom(s => s.Id))
                .ForMember(r => r.InvoiceName, o => o.MapFrom(s => s.InvoiceName))
                .ForMember(r => r.State, o => o.MapFrom(s => s.State))
                .ForMember(r => r.InvoiceItems, o => o.MapFrom(s => s.Items));
        }
    }
}
