using CommandProject.Domain.Entities;
using CommandProject.Infrastructure.Persistence.Configurations;
using CommandProject.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Base;
using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandProject.Infrastructure.Persistence.Context
{
    public class CommandProjectDbContext : DbContext
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }


        private readonly IMediator _mediator;
        private readonly IMessageTypeProviderService _messageTypeProviderService;
        public CommandProjectDbContext(DbContextOptions options, IMediator mediator, IMessageTypeProviderService messageTypeProviderService) : base(options)
        {
            _mediator = mediator;
            _messageTypeProviderService = messageTypeProviderService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this, _messageTypeProviderService);

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            _mediator.DispatchDomainEvents(this, _messageTypeProviderService).GetAwaiter().GetResult();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _mediator.DispatchDomainEvents(this, _messageTypeProviderService).GetAwaiter().GetResult();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
