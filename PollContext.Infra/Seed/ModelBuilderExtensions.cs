using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Entities;
using PollContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollContext.Infra.Seed
{

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Poll poll = new Poll(new DescriptionVO("poll 1"));
            poll.addOptions(new OptionPoll(new DescriptionVO("opt1")));
            poll.addOptions(new OptionPoll(new DescriptionVO("opt2")));
            modelBuilder.Entity<Poll>().HasData(poll);
        }
    }
}