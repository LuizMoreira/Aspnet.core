using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using PollContext.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace PollContext.Test.FakeRepositories
{
    public class FakePollRepository : IPollRepository
    {
        private Poll poll;

        public async Task Create(Poll poll)
        {
            await Task.Delay(10);
        }

        public async Task<Poll> GetById(Guid id)
        {
            poll = new Poll(new DescriptionVO("novo"));
            await Task.Delay(10);
            return poll;
        }

        public async Task Update(Poll poll)
        {
             await Task.Delay(10);
        }
    }
}
