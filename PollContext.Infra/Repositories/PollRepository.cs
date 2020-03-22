using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using PollContext.Domain.Queries;
using PollContext.Domain.Repositories;
using PollContext.Infra.Contexts;
using System;
using System.Linq;

namespace PollContext.Infra.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly DataContext _context;

        public PollRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void Create(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

      
        public Poll GetById(Guid id)
        {
            return _context.Polls.Where(PollQueries.GetById(id)).FirstOrDefault();//.Include(p => p.OptionsPoll).FirstOrDefault();
        }

        public void Update(Poll poll)
        {
            _context.Entry(poll).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
