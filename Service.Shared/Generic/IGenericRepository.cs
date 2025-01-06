using Microsoft.EntityFrameworkCore;
using Service.Shared.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.Generic
{
    public interface IGenericRepository<T,TDBContext> :IDapperRepository where T : class where TDBContext : DbContext
    {
        Task<bool> Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        Task<T> GetbyId(int id);
        Task<List<T>> Getdata();
    }
}
