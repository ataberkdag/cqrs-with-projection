using CommandProject.Domain.Interfaces;
using CommandProject.Domain.Interfaces.Repositories;
using CommandProject.Infrastructure.Persistence.Context;
using CommandProject.Infrastructure.Persistence.Repositories;
using CommandProject.Infrastructure.Persistence.UnitOfWork;
using CommandProject.Infrastructure.Services.Impl;
using CommandProject.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Interfaces;

namespace CommandProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<CommandProjectDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("CommandProjectDb")));

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

            services.AddScoped<ICommandProjectUnitOfWork, CommandProjectUnitOfWork>();

            services.AddScoped<IMessageTypeProviderService, MessageTypeProviderService>();

            return services;
        }
    }
}
