using System;

namespace PollContext.Domain.Queries.PollQueriesResult
{
    public class GetOptionsPollByPolIdQueryResult 
    {
        public GetOptionsPollByPolIdQueryResult(Guid option_id, int qty)
        {
            Option_id = option_id;
            Qty = qty;
        }

        public Guid Option_id { get; set; }

        public int Qty { get; set; }


    }
}
