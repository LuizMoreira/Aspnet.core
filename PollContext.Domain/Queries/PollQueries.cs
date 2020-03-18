using PollContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PollContext.Domain.Queries
{
    public static class PollQueries
    {

        public static Expression<Func<Poll, bool>> GetById(Guid id)
        {
            return p => p.Id == id;
        }
    }
}
