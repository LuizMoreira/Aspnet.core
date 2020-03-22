using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollContext.Domain.Entities;

namespace PollContext.Infra.Mappings
{
    public class OptionPollMap : IEntityTypeConfiguration<OptionPoll>
    {
        public void Configure(EntityTypeBuilder<OptionPoll> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.OwnsOne(x => x.Description)
                .Property(x => x.Description)
                .HasColumnName("Description");

            builder.Property(c => c.Qty)
                .HasColumnName("Qty");

        }
    }
}
