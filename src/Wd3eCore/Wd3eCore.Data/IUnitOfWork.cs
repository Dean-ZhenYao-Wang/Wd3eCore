using System;
using System.Collections.Generic;
using System.Text;

namespace Wd3eCore.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
