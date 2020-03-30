using PollContext.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PollContext.Domain.Repositories
{
    public interface IOptionPollRepository
    {
        void Update(OptionPoll optionPoll);

        Task<OptionPoll> GetOptionPollById(Guid id, Guid poll_Id);

    }
}
