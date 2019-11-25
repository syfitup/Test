using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure
{
    public abstract class DisposableObject : IDisposable
    {
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                DisposeManaged();
            }

            DisposeUnmanaged();

            _disposed = true;
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnmanaged()
        {
        }
    }
}
