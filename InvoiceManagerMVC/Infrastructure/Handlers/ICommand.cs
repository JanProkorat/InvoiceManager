using MediatR;

namespace InvoiceManagerMVC.Infrastructure.Handlers;

public interface ICommand<out TResult> : IRequest<TResult>
{

}