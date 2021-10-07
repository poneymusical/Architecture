using System.Threading;
using System.Threading.Tasks;
using Application.SeedWork;
using Domain.Aggregates;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Commands.CustomerContext
{
    public class CreateCustomer : IRequestHandler<CreateCustomerCommand, OneOf<Customer, ValidationResult, Conflict<Customer>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IValidator<CreateCustomerCommand> _validator;

        public CreateCustomer(IApplicationDbContext dbContext, IValidator<CreateCustomerCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<OneOf<Customer, ValidationResult, Conflict<Customer>>> Handle
            (CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return validationResult;
            
            var customer = new Customer(request.Name);
            
            var conflicting = await _dbContext.Set<Customer>().FirstOrDefaultAsync(Customer.Equals(customer), cancellationToken);
            if (conflicting != null)
                return new Conflict<Customer>(conflicting);

            await _dbContext.Set<Customer>().AddAsync(customer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return customer;
        }
    }

    public record CreateCustomerCommand(string Name) 
        : IRequest<OneOf<Customer, ValidationResult, Conflict<Customer>>>;

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