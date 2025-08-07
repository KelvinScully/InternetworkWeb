using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    internal class DataAccessService(ConnectionOptions connectionoptions)
    {
        public string ConnectionString => _ConnectionOptions.ConnectionString;

        private readonly ConnectionOptions _ConnectionOptions = connectionoptions;

        public async Task<T> GetSqlSingleAsync<T>(string storedProcedure, IEnumerable<SqlParameter> parameters, Dictionary<string, string> propertyMap) where T : class, new()
        {
            var result = new T();

            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure };
            if (parameters != null)
                cmd.Parameters.AddRange([.. parameters]);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            var props = typeof(T).GetProperties();

            while (await reader.ReadAsync())
            {
                var obj = new T();
                foreach (var prop in props)
                {
                    if (!propertyMap.TryGetValue(prop.Name, out var columnName))
                        continue; // skip if not mapped

                    int ordinal;
                    try { ordinal = reader.GetOrdinal(columnName); }
                    catch (IndexOutOfRangeException) { continue; } // skip if column doesn't exist

                    if (reader.IsDBNull(ordinal))
                        continue;

                    var value = reader.GetValue(ordinal);
                    if (value != DBNull.Value)
                        prop.SetValue(obj, value);

                }
                result = obj;
            }
            return result;
        }
        public async Task<List<T>> GetSqlListAsync<T>(string storedProcedure, IEnumerable<SqlParameter> parameters, Dictionary<string, string> propertyMap) where T : class, new()
        {
            var results = new List<T>();

            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure };
            if (parameters != null)
                cmd.Parameters.AddRange([.. parameters]);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            var props = typeof(T).GetProperties();

            while (await reader.ReadAsync())
            {
                var obj = new T();
                foreach (var prop in props)
                {
                    if (!propertyMap.TryGetValue(prop.Name, out var columnName))
                        continue; // skip if not mapped

                    int ordinal;
                    try { ordinal = reader.GetOrdinal(columnName); }
                    catch (IndexOutOfRangeException) { continue; } // skip if column doesn't exist

                    if (reader.IsDBNull(ordinal))
                        continue;

                    var value = reader.GetValue(ordinal);
                    if (value != DBNull.Value)
                        prop.SetValue(obj, value);

                }
                results.Add(obj);
            }
            return results;
        }

        // For inserts returning an ID
        public Task<int> InsertSqlAndReturnIdAsync(string storedProcedure, IEnumerable<SqlParameter> parameters)
            => ExecuteSqlScalarAsync<int>(storedProcedure, parameters);

        public Task<bool> InsertSqlAsync(string storedProcedure, IEnumerable<SqlParameter> parameters)
            => ExecuteNonQueryAsync(storedProcedure, parameters).ContinueWith(t => t.Result > 0);

        public Task<bool> UpdateSqlAsync(string storedProcedure, IEnumerable<SqlParameter> parameters)
            => ExecuteNonQueryAsync(storedProcedure, parameters).ContinueWith(t => t.Result > 0);

        public Task<bool> DeleteSqlAsync(string storedProcedure, IEnumerable<SqlParameter> parameters)
            => ExecuteNonQueryAsync(storedProcedure, parameters).ContinueWith(t => t.Result > 0);

        public Task<bool> ActivateSqlAsync(string storedProcedure, IEnumerable<SqlParameter> parameters)
            => ExecuteNonQueryAsync(storedProcedure, parameters).ContinueWith(t => t.Result > 0);
        private async Task<int> ExecuteNonQueryAsync(string storedProcedure, IEnumerable<SqlParameter> parameters)
        {
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure };
            if (parameters != null)
                cmd.Parameters.AddRange([.. parameters]);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync(); // Returns affected rows
        }

        // For inserts returning an ID (or any scalar value)
        private async Task<T> ExecuteSqlScalarAsync<T>(
            string storedProcedure,
            IEnumerable<SqlParameter> parameters)
        {
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure };
            if (parameters != null)
                cmd.Parameters.AddRange([.. parameters]);

            await conn.OpenAsync();
            object result = await cmd.ExecuteScalarAsync();
            return (T)Convert.ChangeType(result, typeof(T));
        }

    }

    internal static class DataReaderHelper
    {
        public static T? Get<T>(SqlDataReader reader, string columnName)
        {
            int ordinal;
            try { ordinal = reader.GetOrdinal(columnName); }
            catch (IndexOutOfRangeException) { return default; }

            if (reader.IsDBNull(ordinal))
                return default;

            object value = reader.GetValue(ordinal);

            // Handle byte[] (varbinary) directly
            if (typeof(T) == typeof(byte[]))
                return (T)value;

            // Handles nullable types, enums, etc.
            if (typeof(T).IsEnum)
                return (T)Enum.ToObject(typeof(T), value);

            return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
        }
    }
}