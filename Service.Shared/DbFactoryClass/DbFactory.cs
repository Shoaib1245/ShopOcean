using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.DbFactoryClass
{
    public class DbFactory<TDbContext> : IDisposable where TDbContext : DbContext
    {
        private Func<TDbContext> _funfactory;
        private DbContext _DBContext;

        public DbContext myDb => _DBContext ?? (_DBContext = _funfactory.Invoke());
        public DbFactory(Func<TDbContext> funfactory)
        {
            _funfactory = funfactory;
        }
        public void Dispose()
        {
            if(_DBContext != null)
            {
                _DBContext.Dispose();
            }
        }
    }
}
