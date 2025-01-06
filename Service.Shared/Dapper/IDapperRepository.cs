using Service.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.Dapper
{
    public interface IDapperRepository
    {
        //Task<IEnumerable<T>> SimpleQuery<T>(string Query) where T : class;
        //Task<IEnumerable<T>> ParameterizeQuery<T>(string Query, object? Params) where T : class;
        //Task<T> ParameterizeQueryFirst<T>(string Query, object? Params) where T : class;
        //Task<T> QuerySingleAsync<T>(string query, object? parameters = null);
        //Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null);

        //Task<int> InsertAsync<T>(string tableName, T entity);

        //Task<int> UpdateAsync<T>(string tableName, T entity, string keyColumn);
        Task<int> DataUpdate<T>(string TableName, T Entity, string Id);
        Task<int> SaveData<T>(string TableName, T obj, string keyColumn) ;
        Task<GraphsDTOs> StoreProcedureQuery(string storeprocName, object parameter);
        //  Task<int> DeleteAsync(string tableName, object parameters);

        Task<int> ProfileDataUpdate<T>(string querry, T obj);
    }
}
