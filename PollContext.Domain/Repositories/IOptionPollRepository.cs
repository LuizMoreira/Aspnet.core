using PollContext.Domain.Entities;
using System;

namespace PollContext.Domain.Repositories
{
    public interface IOptionPollRepository
    {
        void Update(OptionPoll optionPoll);

        OptionPoll GetOptionPollById(Guid id, Guid poll_Id);

    }
}
