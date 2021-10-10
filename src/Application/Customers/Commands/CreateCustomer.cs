using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Aggregates;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Customers.Commands
{
    public class CreateCustomer : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateCustomer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name);

            var conflicting = await _dbContext.Set<Customer>().FirstOrDefaultAsync(Customer.Equals(customer), cancellationToken);
            if (conflicting != null)
                throw new ConflictException();

            await _dbContext.Set<Customer>().AddAsync(customer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return customer;
        }
    }

    public record CreateCustomerCommand(string Name) : IRequest<Customer>;

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(Customer.NameMaxLength);
        }
    }
}