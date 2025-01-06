using Microsoft.EntityFrameworkCore;
using Service.Shared.Dapper;
using Service.Shared.DbFactoryClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.Generic
{
    public class GenericRepository<T, TDBContext> : DapperRepository,IGenericRepository<T, TDBContext> where T : class where TDBContext : DbContext
    {
        private readonly DbFactory<TDBContext> _dbFactory;
        private readonly DbSet<T> _dbset;
        public GenericRepository(DbFactory<TDBContext> dbFactory) : base(GETConnection(dbFactory))
        {
            _dbFactory = dbFactory;
            _dbset=_dbFactory.myDb.Set<T>();
        }

        public static string GETConnection(DbFactory<TDBContext> dbFactory)
        {
            return ((TDBContext)dbFactory.myDb).Database.GetDbConnection().ConnectionString;
        }


        public async Task<bool> Add(T entity)
        {
          await _dbset.AddAsync(entity);
            _dbFactory.myDb.SaveChanges();
            return true;
        }

        public bool Delete(T entity)
        {
             _dbset.Remove(entity);
            _dbFactory.myDb.SaveChanges();
            return true;
        }

        public async Task<T> GetbyId(int id)
        {
            var data = await _dbset.FindAsync(id);
                return data;
            
        }

        public async Task<List<T>> Getdata()
        {

            return await _dbset.ToListAsync();
        }

        public bool Update(T entity)
        {
            _dbset.Update(entity);
            _dbFactory.myDb.SaveChanges();
            return true;
        }
    }
}
