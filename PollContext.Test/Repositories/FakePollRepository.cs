using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
        public Poll GetPollById(Guid id)
        {
            return null;
        }
    }
}
