using System;
using System.Collections.Generic;
using System.Text;
using Wd3eCore.Data;

namespace Wd3eCore.CVMDesktop.dbModel
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly EF_Core_Context db;
        private GenericRepository<SwaggerUi> swaggeruiRepository;
        public UnitOfWork(EF_Core_Context session)
        {
            db = session;
        }
        public GenericRepository<SwaggerUi> SwaggerUiRepository
        {
            get
            {
                if (this.swaggeruiRepository == null)
                {
                    this.swaggeruiRepository = new GenericRepository<SwaggerUi>(db);
                }
                return swaggeruiRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
