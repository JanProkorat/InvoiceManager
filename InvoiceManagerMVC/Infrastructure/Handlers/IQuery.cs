using MediatR;

namespace InvoiceManagerMVC.Infrastructure.Handlers
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
        
    }
}