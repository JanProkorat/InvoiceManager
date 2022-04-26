using MediatR;

namespace InvoiceManagerMVC.Infrastructure.Handlers
{
    public interface IHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {

    }
}