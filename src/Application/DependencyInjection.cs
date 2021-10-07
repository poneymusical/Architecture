﻿using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly(); 
            services.AddMediatR(currentAssembly);
            services.AddValidatorsFromAssembly(currentAssembly);
            return services;
        }
    }
}