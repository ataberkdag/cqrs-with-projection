using CommandProject.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Base;
using Shared.Domain.Entities;
using System.Text.Json;

namespace CommandProject.Infrastructure
{
    public static class Extensions
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, DbContext ctx, IMessageTypeProviderService messageTypeProviderService)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseRootEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) => {

                var serializedData = JsonSerializer.Serialize<object>(domainEvent);

                var type = messageTypeProviderService.GetMessageType(domainEvent.GetType());
                string queueName = messageTypeProviderService.GetQueueName(domainEvent.GetType());

                var outboxMessage = OutboxMessage.CreateOutboxMessage(type?.AssemblyQualifiedName, serializedData, queueName);

                await ctx.Set<OutboxMessage>().AddAsync(outboxMessage);
            });

            await Task.WhenAll(tasks);
        }
    }
}
