using PollContext.Domain.Entities;

namespace PollContext.Domain.Repositories
{
    public interface IPollRepository
    {
        void CreatePoll(Poll poll);

        Poll GetPollById(int id);

    }
}
