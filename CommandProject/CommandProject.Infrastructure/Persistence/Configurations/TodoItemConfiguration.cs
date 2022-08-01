using CommandProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandProject.Infrastructure.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Property(x => x.Status);
            builder.Property(x => x.Title).HasMaxLength(75).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
