using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Common.Domain.Operations;

namespace ResponsibleSystem.Common.Data
{
    public class SqlDataService : ISqlDataService
    {
        private readonly SqlDataServiceConfig _config;
        public string ConnectionString => _config?.SQLConnection;

        public SqlDataService(IConfigFactory<SqlDataServiceConfig> configFactory)
        {
            _config = configFactory.GetConfig();
        }

        public OperationResult<T> ExcecuteScalar<T>(string commandSQL, Dictionary<string, object> sqlParameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_config.SQLConnection))
                {
                    using (var command = new SqlCommand(commandSQL, connection))
                    {
                        if (sqlParameters != null)
                        {
                            foreach (var parameter in sqlParameters)
                            {
                                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                            }
                        }

                        connection.Open();
                        var result = command.ExecuteScalar();
                        connection.Close();
                        if (result != null)
                        {
                            return (T)result;
                        }
                        else
                        {
                            return OperationResult.Error($"Can't find any records >> {commandSQL}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Exceprion occured exception: '{ex.Message}'");
            }
        }

        public OperationResult<int> ExecuteNonQuery(string commandSQL, Dictionary<string, object> sqlParameters = null)
        {
            try
            {
                var affectedRecords = 0;
                using (var connection = new SqlConnection(_config.SQLConnection))
                {
                    using (var command = new SqlCommand(commandSQL, connection))
                    {
                        if (sqlParameters != null)
                        {
                            foreach (var parameter in sqlParameters)
                            {
                                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                            }
                        }

                        connection.Open();
                        affectedRecords = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return affectedRecords;
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Exceprion occured exception: '{ex.Message}'");
            }
        }

        public OperationResult<T> Get<T>(Func<SqlConnection, OperationResult<T>> func)
        {
            try
            {
                using (var connection = new SqlConnection(_config.SQLConnection))
                {
                    return func.Invoke(connection);
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Der opstod en exception: '{ex.Message}'");
            }
        }
    }
}
