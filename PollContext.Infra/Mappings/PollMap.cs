using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollContext.Domain.Entities;
using System;

namespace PollContext.Infra.Mappings
{
    public class PollMap : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Description)
                .HasColumnName("Description");

            builder.Property(c => c.Views)
                .HasColumnName("View");

            builder.HasMany(x => x.OptionsPoll).WithOne(b => b.Poll).HasForeignKey(a => a.Poll_Id);

        }
    }
}
