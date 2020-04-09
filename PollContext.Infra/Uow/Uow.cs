using Microsoft.EntityFrameworkCore.Storage;
using PollContext.Infra.Contexts;
using System;
using PollContext.Domain.Repositories;

namespace PollContext.Infra.Uow
{
    public class Uow : IUow, IDisposable
    {
        private IDbContextTransaction _transaction;
        public DataContext _context { get; }

        public Uow(DataContext context)
        {
            _context = context;
        }



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

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
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
