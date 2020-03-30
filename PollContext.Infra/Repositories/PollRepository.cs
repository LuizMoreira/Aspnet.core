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
    public class PollRepository : IPollRepository
    {
        private readonly DataContext _context;

        public PollRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task Create(Poll poll)
        {
            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
        }

      
        public async Task<Poll> GetById(Guid id)
        {
            return await _context.Polls.Where(PollQueries.GetById(id)).Include(p => p.OptionsPoll).FirstOrDefaultAsync();
        }

        public Task Update(Poll poll)
        {
            try
            {
                _context.Entry(poll).State = EntityState.Modified;
                return _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
