using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using System;

namespace PollContext.Test.Repositories
{
    public class FakePollRepository : IPollRepository
    {
        public void Create(Poll poll)
        {
        }

        public void Update(Poll poll)
        {

        }
        public Poll GetById(Guid id)
        {
            return null;
        }
    }
}
