using PollContext.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace PollContext.Domain.Queries
{
    public static class OptionPollQueries
    {
        public static Expression<Func<OptionPoll, bool>> GetById(Guid id, Guid poll_Id)
        {
            return x => x.Id == id && x.Poll_Id == poll_Id;
        }
    }
}
