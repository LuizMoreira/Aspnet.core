using PollContext.Shared.Queries.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Queries.PollQueriesResult
{
    public class GetPollStatsByIdQueryResult : IQueryResult
    {
        public GetPollStatsByIdQueryResult(Guid poll_id, int views)
        {
            Poll_id = poll_id;
            Views = views;
            options = new List<GetOptionsPollByPolIdQueryResult>();
        }

        public Guid Poll_id { get; set; }

        public int Views { get; set; }

        public List<GetOptionsPollByPolIdQueryResult> options { get; set; }


    }
}
