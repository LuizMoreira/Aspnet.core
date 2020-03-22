using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollContext.Domain.Entities;

namespace PollContext.Infra.Mappings
{
    public class PollMap : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.OwnsOne(x => x.Description)
                .Property(x => x.Description)
                .HasColumnName("Description");

            builder.Property(c => c.Views)
                .HasColumnName("View");

            //builder.HasMany(x => x.OptionsPoll).WithOne(b => b.Poll).HasForeignKey(a => a.Poll_Id);

            var navigation =
              builder.Metadata.FindNavigation(nameof(Poll.OptionsPoll));

            //EF access the OrderItem collection property through its backing field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
