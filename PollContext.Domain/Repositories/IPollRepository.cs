using PollContext.Domain.Entities;
using System;
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
