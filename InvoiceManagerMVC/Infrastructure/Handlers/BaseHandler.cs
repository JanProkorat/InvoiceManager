using System.Threading;
using System.Threading.Tasks;
using InvoiceManagerMVC.Infrastructure.UnitOfWork;
using MediatR;

namespace InvoiceManagerMVC.Infrastructure.Handlers
{
    /// <summary>
    /// Handler class instance
    /// </summary>
    /// <typeparam name="TRequest">Command or query</typeparam>
    /// <typeparam name="TResult">Response dto</typeparam>
    public abstract class BaseHandler<TRequest, TResult> : IHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        
        /// <summary>
        /// Unit of work instance bound to current execution scope.
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;
        
        public BaseHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

        }


        /// <summary>
        /// Handles the operation
        /// </summary>
        /// <param name="request">Request for the operation to execute.</param>
        /// <param name="cancellationToken">Reference to notification that operations should be canceled.</param>
        /// <returns>A task representing asynchronous operation.</returns>
        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            await Validate(request, cancellationToken);
            return await Write(request, cancellationToken);
        }
        
        /// <summary>
        /// Validates data needed for operation execution in Write method
        /// </summary>
        /// <param name="command">Command for the operation to execute.</param>
        /// <param name="cancellationToken">Reference to notification that operations should be canceled.</param>
        /// <returns>A task representing asynchronous operation.</returns>
        public abstract Task Validate(TRequest command, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the command actions
        /// </summary>
        /// <param name="command">Command for the operation to execute.</param>
        /// <param name="cancellationToken">Reference to notification that operations should be canceled.</param>
        /// <returns></returns>
        public abstract Task<TResult> Write(TRequest command, CancellationToken cancellationToken);
    }
}