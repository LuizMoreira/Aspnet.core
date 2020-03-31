using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Entities;
using PollContext.Domain.ValueObjects;
using PollContext.Infra.Mappings;
using PollContext.Infra.Seed;

namespace PollContext.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new PollMap());
            modelBuilder.ApplyConfiguration(new OptionPollMap());

            //modelBuilder.Seed();
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<OptionPoll> OptionsPoll { get; set; }


    }
}
