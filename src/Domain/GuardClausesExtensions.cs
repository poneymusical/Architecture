using System;
using Ardalis.GuardClauses;

namespace Domain
{
    internal static class GuardClausesExtensions
    {
        internal static string EmptyOrTooLong(this IGuardClause guardClause, string? input, int maxLength, string parameterName, string? message = null)
        {
            Guard.Against.NullOrWhiteSpace(input, parameterName);
            if (input.Length > maxLength)
                throw new ArgumentException(message ?? $"Required input {parameterName} was too long.", parameterName);
            return input;
        }
    }
}