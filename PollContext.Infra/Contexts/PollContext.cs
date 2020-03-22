using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Entities;
using PollContext.Infra.Mappings;

namespace PollContext.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<OptionPoll> OptionsPoll { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new PollMap());
            modelBuilder.ApplyConfiguration(new OptionPollMap());

        }
       
    }
}
