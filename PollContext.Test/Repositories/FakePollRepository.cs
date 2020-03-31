using PollContext.Domain.Entities;
using PollContext.Domain.Queries;
using PollContext.Domain.Repositories;
using PollContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PollContext.Test.Repositories
{
    public class FakePollRepository : IPollRepository
    {
        private Poll poll;



        public FakePollRepository()
        {
            poll = new Poll(new DescriptionVO("poll 1"));
            var opt1 = new OptionPoll(new DescriptionVO("opt 3"));
            opt1.increaseQty();
            poll.addOptions(opt1);
            poll.increaseView();
            var opt2 = new OptionPoll(new DescriptionVO("opt 3"));
            opt2.increaseQty();
            poll.addOptions(opt2);

        }


        public async Task Create(Poll poll)
        {
        }

        public async Task Update(Poll poll)
        {

        }
        public async Task<Poll> GetById(Guid id)
        {
            var poll = new Poll(new DescriptionVO("poll 1"));
            var opt1 = new OptionPoll(new DescriptionVO("opt 3"));
            opt1.increaseQty();
            poll.addOptions(opt1);
            poll.increaseView();
            var opt2 = new OptionPoll(new DescriptionVO("opt 3"));
            opt2.increaseQty();
            poll.addOptions(opt2);

            return poll;
        }
    }
}
