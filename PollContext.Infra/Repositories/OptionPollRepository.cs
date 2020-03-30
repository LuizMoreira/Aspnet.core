using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Entities;
using PollContext.Domain.Queries;
using PollContext.Domain.Repositories;
using PollContext.Infra.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollContext.Infra.Repositories
{
    public class OptionPollRepository : IOptionPollRepository
    {

        private readonly DataContext _context;

        public OptionPollRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Task<OptionPoll> GetOptionPollById(Guid id, Guid poll_Id)
        {
            return _context.OptionsPoll.FirstOrDefaultAsync(OptionPollQueries.GetById(id, poll_Id));
        }

        public void Update(OptionPoll optionPoll)
        {
            try
            {
                _context.Entry(optionPoll).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
