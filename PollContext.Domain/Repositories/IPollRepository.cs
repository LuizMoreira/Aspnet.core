using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PollContext.Domain.Repositories
{
    public interface IPollRepository
    {
        Task Create(Poll poll);
        
        Task Update(Poll poll);

        Task<Poll> GetById(Guid id);

    }
}
