using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PollContext.Domain.Queries;
using PollContext.Domain.Queries.PollQueriesInput;
using PollContext.Domain.Queries.PollQueriesResult;
using PollContext.Domain.Repositories;
using PollContext.Infra.Contexts;
using PollContext.Shared.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollContext.Infra.Repositories
{
    public class PollQueryRepository : IPollQueryRepository
    {


        //TODO:APÓS CRIAR BANCO SOMENTE DE LEITURA, ALTERAR O DATACONTEXT
        private readonly DataContext _context;
        private readonly ILogger _logger;


        public PollQueryRepository(DataContext dataContext, ILoggerFactory logger)
        {
            _context = dataContext;
            _logger = logger.CreateLogger("PollContext.Infra.Repositories");

        }
        

        public async Task<GenericQueryResult> GetStatsById(GetPollStatsQuery query)
        {
                query.Validate();
                if (query.Invalid)
                    return new GenericQueryResult(true, "Enquete inválida", query.Notifications);

                var poll = await _context.Polls.Where(PollQueries.GetById(query.Poll_Id)).Include(p => p.OptionsPoll).FirstOrDefaultAsync();

                if (poll == null) return new GenericQueryResult(true, "Enquete não encontrada", null);

                GetPollStatsByIdQueryResult getPollStatsByIdQueryResult = new GetPollStatsByIdQueryResult(poll.Id, poll.Views);
                foreach (var item in poll.OptionsPoll)
                {
                    getPollStatsByIdQueryResult.options.Add(new GetOptionsPollByPolIdQueryResult(item.Id, item.Qty));
                }

                return new GenericQueryResult(true, "Status da enquete obtida com sucesso", getPollStatsByIdQueryResult);
        }


           
    }
}
