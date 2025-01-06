using Dapper;
using Microsoft.Data.SqlClient;
using Service.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Shared.Dapper
{
    public class DapperRepository:IDisposable ,IDapperRepository
    {
        private readonly string _connection;
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connection);
        }
        public DapperRepository(string connection)
        {
            _connection = connection;
        }

        //public async Task<IEnumerable<T>> ParameterizeQuery<T>(string Query, object? Params) where T : class
        //{
        //    try
        //    {
        //        using (var connection = CreateConnection())
        //        {
        //            var data = await connection.QueryAsync<T>(Query, Params);
        //            return data;
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public async Task<T> ParameterizeQueryFirst<T>(string Query, object? Params) where T : class
        //{
        //    using (var connection = CreateConnection())
        //    {
        //        var data = await connection.QueryFirstAsync<T>(Query, Params);
        //        return data;
        //    }

        //}

        public async Task<IEnumerable<T>> SimpleQuery<T>(string Query) where T : class
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await (connection.QueryAsync<T>(Query).ConfigureAwait(false));
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<GraphsDTOs> StoreProcedureQuery(string storeprocName ,object parameter)
        {
            try
            {
                using (var connection = CreateConnection())
                {
       
                    var graphData = await (connection.QuerySingleOrDefaultAsync<GraphsDTOs>(storeprocName,parameter,commandType:System.Data.CommandType.StoredProcedure).ConfigureAwait(false));
                    return graphData ?? new GraphsDTOs();
                }
              

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<AdminGraphsDTOs> StoreProceduredata(string storeprocName)
        {
            try
            {
                using (var connection = CreateConnection())
                {

                    var graphData = await (connection.QuerySingleOrDefaultAsync<AdminGraphsDTOs>(storeprocName, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false));
                    return graphData ?? new AdminGraphsDTOs();
                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> ExecuteStoreProcedureQuery(string storeprocName,object parameter)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.ExecuteAsync(storeprocName, parameter, commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                    return true;
                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<IEnumerable<T>> GetDatabyId<T>(string Query,object parameter)
        {
            
            try
            {
                using (var connection = CreateConnection())
                {
                    return await (connection.QueryAsync<T>(Query, parameter));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<T> GetSingleDatabyId<T>(string Query, object parameter)
        {

            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(Query, parameter);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        //public async Task<int> ExecuteAsync(string query, object? parameters = null)
        //{
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.ExecuteAsync(query, parameters);
        //    }
        //}

        //public async Task<T> QuerySingleAsync<T>(string query, object? parameters = null)
        //{
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
        //    }
        //}

        //public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null)
        //{
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.QueryAsync<T>(query, parameters);
        //    }
        //}

        //public async Task<int> InsertAsync<T>(string tableName, T entity)
        //{
        //    var query = GenerateInsertQuery(tableName, entity);
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.ExecuteAsync(query, entity);
        //    }
        //}

        //public async Task<int> UpdateAsync<T>(string tableName, T entity, string keyColumn)
        //{
        //    var prope = typeof(T).GetProperties();
        //    var prop = typeof(T).GetProperties().Where(p != prop).Select(p => p.Name);
        //    var join=string.Join(",", prop);
        //    var dd = string.Join(",", prop.Where(p=>p !=keyColumn).Select(p=> $"{p}=@{p}"));
        //    var query = GenerateUpdateQuery(tableName, entity, keyColumn);
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.ExecuteAsync(query, entity);
        //    }
        //}

        public async Task<int> SaveData<T>(string TableName,T obj,string keyColumn) 
        {
            var prop = typeof(T).GetProperties().Select(p=>p.Name);
            var columns = string.Join(",", prop.Where(prop => prop != keyColumn));
            var value = string.Join(",", prop.Where(prop => prop != keyColumn).Select(prop => $"@{prop}"));
            var querry = $"insert into {TableName} ({columns}) values ({value})";
            using (var conn = CreateConnection())
            {
                return await conn.ExecuteAsync(querry, obj);
            }
        }


        public async Task<int> ProfileDataUpdate<T>(string querry, T obj)
        {
            using (var conn = CreateConnection())
            {
                return await conn.ExecuteAsync(querry, obj);
            }
        }


        public async Task<int> DataUpdate<T>(string TableName, T Entity, string Email)
        {
            var property = typeof(T).GetProperties();
            var prop = typeof(T).GetProperties().Select(a => a.Name);
            var propp = string.Join(",",prop.Where(a => a != Email).Select(p => $"{p} = @{p}"));
            var querry = $"Update {TableName} set {propp} where {Email}=@{Email}";
            using (var Connection=CreateConnection())
                {
               return await Connection.ExecuteAsync(querry, Entity);
                }
            
        }






        //public async Task<int> DeleteAsync(string tableName, object parameters)
        //{
        //    var query = $"DELETE FROM {tableName} WHERE {GenerateWhereClause(parameters)}";
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.ExecuteAsync(query, parameters);
        //    }
        //}

        //private string GenerateInsertQuery<T>(string tableName, T entity)
        //{
        //    var properties = typeof(T).GetProperties().Select(p => p.Name);
        //    var columns = string.Join(", ", properties);
        //    var values = string.Join(", ", properties.Select(p => "@" + p));
        //    return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        //}

        //private string GenerateUpdateQuery<T>(string tableName, T entity, string keyColumn)
        //{
        //    var properties = typeof(T).GetProperties().Select(p => p.Name);
        //    var setClause = string.Join(", ", properties.Where(p => p != keyColumn).Select(p => $"{p} = @{p}"));
        //    return $"UPDATE {tableName} SET {setClause} WHERE {keyColumn} = @{keyColumn}";
        //}

        //private string GenerateWhereClause(object parameters)
        //{
        //    var properties = parameters.GetType().GetProperties().Select(p => p.Name);
        //    return string.Join(" AND ", properties.Select(p => $"{p} = @{p}"));
        //}

        public void Dispose()
        {
            // Dispose resources if necessary
        }
    }
}
