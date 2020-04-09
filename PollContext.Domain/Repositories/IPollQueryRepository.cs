using PollContext.Domain.Queries.PollQueriesInput;
using PollContext.Shared.Queries;
using System.Threading.Tasks;

namespace PollContext.Domain.Repositories
{
    public interface IPollQueryRepository
    {
        Task<GenericQueryResult> GetStatsById(GetPollStatsQuery query);

    }
}
