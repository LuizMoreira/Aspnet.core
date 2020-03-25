using Microsoft.EntityFrameworkCore.Storage;
using PollContext.Domain.Repositories;
using PollContext.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollContext.Infra.Uow
{
    interface IUow
    {
        IPollRepository PollRepository { get; }
        IOptionPollRepository OptionPollRepository { get; }
        void SaveChanges();
        DataContext _context { get; }

        IDbContextTransaction BeginEfTransaction();
        void CommitEfTransaction();
        void RollbackEfTransaction();
    }
}
