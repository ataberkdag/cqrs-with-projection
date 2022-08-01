using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities;

namespace CommandProject.Infrastructure.Persistence.Configurations
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.QueueName).IsRequired(false);
        }
    }
}
