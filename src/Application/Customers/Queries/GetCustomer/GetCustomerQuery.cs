using Application.Common.Exceptions;
using Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomer
{
    public record GetCustomerQuery(Guid Id) : IRequest<Customer>
    {
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCustomerQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (customer == null)
                throw new NotFoundException(nameof(Customer), request.Id);

            return customer;
        }
    }
}
