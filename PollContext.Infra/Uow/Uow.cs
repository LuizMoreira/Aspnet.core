using Microsoft.EntityFrameworkCore.Storage;
using PollContext.Domain.Repositories;
using PollContext.Infra.Contexts;
using PollContext.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollContext.Infra.Uow
{
    public class Uow : IUow, IDisposable
    {
        private IDbContextTransaction _transaction;
        private PollRepository _pollRepository = null;
        private OptionPollRepository _optionPollRepository = null;

        public DataContext _context { get; }

        public Uow(DataContext context)
        {
            _context = context;
        }


        #region Repositories

        /// <summary>
        /// Poll Repository
        /// </summary>
        public IPollRepository PollRepository
        {
            get
            {
                if (_pollRepository == null)
                {
                    _pollRepository = new PollRepository(_context);
                }
                return _pollRepository;
            }
        }

        /// <summary>
        /// Options poll
        /// </summary>
        public IOptionPollRepository OptionPollRepository
        {
            get
            {
                if (_optionPollRepository == null)
                {
                    _optionPollRepository = new OptionPollRepository(_context);
                }
                return _optionPollRepository;
            }
        }

        #endregion


        public IDbContextTransaction BeginEfTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
            return _transaction;
        }

        public void CommitEfTransaction()
        {
            if (_transaction == null) throw new Exception("No transaction");

            _transaction.Commit();
            _transaction = null;
        }

        public void RollbackEfTransaction()
        {
            if (_transaction == null) throw new Exception("No transaction");

            _transaction.Rollback();
            _transaction = null;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        #region disposed

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
