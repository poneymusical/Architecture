using System;
using System.Linq.Expressions;
using Ardalis.GuardClauses;
using Domain.SeedWork;

namespace Domain.Aggregates
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public Customer(string name)
        {
            Name = Guard.Against.EmptyOrTooLong(name, NameMaxLength, nameof(name));
        }

        public const int NameMaxLength = 100;

        public static Expression<Func<Customer, bool>> Equals(Customer other) => 
            customer => customer.Id != other.Id && customer.Name == other.Name;
    }
}