using System;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;

namespace FollowTheTask.DAL.Repositories
{
    public abstract class Repository : IRepository
    {
        protected readonly FollowTheTaskContext Context;


        protected bool Disposed { get; private set; }


        protected Repository(FollowTheTaskContext context)
        {
            Context = context;
            Disposed = false;
        }

        ~Repository()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return Context.SaveChangesAsync();
        }


        protected void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects.
                DisposeManagedOverride();
            }

            // Free any unmanaged objects.
            DisposeUnManagedOverride();
            Disposed = true;
        }

        protected virtual void DisposeManagedOverride()
        {
        }

        protected virtual void DisposeUnManagedOverride()
        {
        }
    }
}