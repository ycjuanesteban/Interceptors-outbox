using EFInterceptor.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFInterceptor.Infrastructure.Persistence.DataConfiguration
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        void IEntityTypeConfiguration<OutboxMessage>.Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("OutboxMessages");
        }
    }
}
