using PollContext.Domain.Entities;
using System;

namespace PollContext.Domain.Repositories
{
    public interface IPollRepository
    {
        void Create(Poll poll);
        
        void Update(Poll poll);

        Poll GetPollById(Guid id);

    }
}
