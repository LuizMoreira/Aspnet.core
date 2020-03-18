using Microsoft.EntityFrameworkCore;
using PollContext.Domain.Entities;
using PollContext.Domain.Queries;
using PollContext.Domain.Repositories;
using PollContext.Infra.Contexts;
using System;
using System.Linq;

namespace PollContext.Infra.Repositories
{
    public class OptionPollRepository : IOptionPollRepository
    {

        private readonly DataContext _context;

        public OptionPollRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public OptionPoll GetOptionPollById(Guid id, Guid poll_Id)
        {
            return _context.OptionsPoll.FirstOrDefault(OptionPollQueries.GetById(id, poll_Id));
        }

        public void Update(OptionPoll optionPoll)
        {
            _context.Entry(optionPoll).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
