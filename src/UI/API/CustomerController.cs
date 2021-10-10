using System;
using System.Threading.Tasks;
using Application;
using Application.Customers.Commands;
using Application.Customers.Queries.GetCustomer;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Helpers;

namespace UI.API
{
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _applicationDbContext;

        public CustomerController(IMediator mediator, IApplicationDbContext applicationDbContext)
        {
            _mediator = mediator;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _applicationDbContext.Set<Customer>().ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetCustomerQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id}, result);
        }
    }
}