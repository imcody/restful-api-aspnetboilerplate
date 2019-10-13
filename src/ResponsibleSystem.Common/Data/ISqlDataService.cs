using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ResponsibleSystem.Common.Domain.Operations;

namespace ResponsibleSystem.Common.Data
{
    public interface ISqlDataService
    {
        string ConnectionString { get; }
        OperationResult<T> ExcecuteScalar<T>(string commandSQL, Dictionary<string, object> sqlParameters = null);
        OperationResult<int> ExecuteNonQuery(string commandSQL, Dictionary<string, object> sqlParameters = null);
        OperationResult<T> Get<T>(Func<SqlConnection, OperationResult<T>> func);
    }
}
