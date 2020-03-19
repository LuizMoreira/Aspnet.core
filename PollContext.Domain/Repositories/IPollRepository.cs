using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace PollContext.Domain.Repositories
{
    public interface IPollRepository
    {
        void Create(Poll poll);
        
        void Update(Poll poll);
        
        Poll GetById(Guid id);

    }
}
