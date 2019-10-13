using ResponsibleSystem.Common.CosmosDb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ResponsibleSystem.Common.CosmosDb.Services
{
    public class CosmoDbSqlQueryBuilder
    {
        public virtual IList<CosmosDbSqlProperty> GetSelectProperties(Type type)
        {
            var properties =
                type
                    .GetProperties()
                    .Where(x => x.GetCustomAttribute<SqlSelectPropertyAttribute>() != null)
                    .ToArray();

            var result = new List<CosmosDbSqlProperty>();

            foreach (var propertyInfo in properties)
            {
                var sqlProperty = new CosmosDbSqlProperty();

                var sqlSelectAttribute = propertyInfo.GetCustomAttribute<SqlSelectPropertyAttribute>();

                if (string.IsNullOrWhiteSpace(sqlSelectAttribute.Name))
                    sqlProperty.Alias = propertyInfo.Name;
                else
                    sqlProperty.Alias = sqlSelectAttribute.Name;

                if (sqlProperty.Alias == "Id")
                    sqlProperty.Alias = "id";

                // we need to look at inner props

                if (sqlSelectAttribute.UseInnerProps)
                {
                    sqlProperty.InnerProps = GetSelectProperties(propertyInfo.PropertyType);
                }

                result.Add(sqlProperty);
            }
            return result;
        }

        public virtual StringBuilder GetSelectStatement(Type type)
        {
            var props = GetSelectProperties(type);
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            if (props == null || props.Count == 0)
            {
                sb.Append("* ");
            }
            else
            {
                foreach (var p in props)
                {
                    // TODO : update to nth level deep
                    if (p.InnerProps != null && p.InnerProps.Count > 0)
                    {
                        sb.Append($"(NOT IS_DEFINED(c.{p.Alias}) OR c.{p.Alias} = null) ? null : ");
                        sb.Append("{ ");
                        foreach (var innerProp in p.InnerProps)
                        {
                            sb.Append($"{innerProp.Alias}: c.{p.Alias}.{innerProp.Alias}, ");
                        }
                        sb.Remove(sb.Length - 2, 2);
                        sb.Append(" } AS ").Append($"{p.Alias}, ");
                    }
                    else
                    {
                        var sep = p.Alias.StartsWith("c.") ? "" : "c.";
                        sb.Append($"{sep}{p.Alias}, ");
                    }
                }
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(" FROM c");
            return sb;
        }

        public virtual StringBuilder GetSelectStatement(string sqlQueryExpression)
        {
            var sb = new StringBuilder();
            sb.Append(!sqlQueryExpression.StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase)
                ? $"SELECT * FROM c WHERE {sqlQueryExpression}"
                : sqlQueryExpression);

            return sb;
        }

        public virtual StringBuilder GetWhereConditional(StringBuilder sqlQuery, Type type, string collectionId = null)
        {
            var sqlQueryExpression = sqlQuery.ToString();
            if (string.IsNullOrWhiteSpace(collectionId) && !sqlQueryExpression.ToLower().Contains("AND c.Entity"))
            {
                var sep = sqlQueryExpression.ToLower().Contains(" where ") ? " AND " : " WHERE ";
                var typeFilter = $" {sep} c.Entity = '{type.Name}' ";
                sqlQuery = new StringBuilder();
                sqlQuery.Append(
                    sqlQueryExpression.ToLower().Contains("order by") ?
                        sqlQueryExpression.Insert(sqlQueryExpression.ToLower().IndexOf("order by"), typeFilter) :
                        sqlQueryExpression + typeFilter
                    );
            }

            return sqlQuery;
        }
    }
}