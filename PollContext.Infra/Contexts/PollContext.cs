using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Entities;

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
            //pkg Relational para anotações de tabela e colunas. 
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<Poll>().Property(x => x.Id);
            modelBuilder.Entity<Poll>().Property(x => x.Description).HasMaxLength(150).HasColumnType("varchar(150");
            modelBuilder.Entity<Poll>().Property(x => x.Views);


            modelBuilder.Entity<OptionPoll>().ToTable("OptionPoll");
            modelBuilder.Entity<OptionPoll>().Property(x => x.Id);
            modelBuilder.Entity<OptionPoll>().Property(x => x.Poll_Id).IsRequired();
            modelBuilder.Entity<OptionPoll>().Property(x => x.Description).HasMaxLength(150).HasColumnType("varchar(150");
            modelBuilder.Entity<OptionPoll>().Property(x => x.Qty);

        }
    }
}
