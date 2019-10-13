using Autofac;
using System;
using System.Data.SqlClient;
using ResponsibleSystem.EntityFrameworkCore;

namespace ResponsibleSystem.Common.Data
{
    /// <summary>
    /// Business module
    /// </summary>
    public class DbContextModule : Module
    {
        private readonly string _connectionString;

        /// <summary>
        /// Business module
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="entityConnectionStringName"></param>
        public DbContextModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Load composition
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // IDbConnection:
            builder.Register(c =>
            {
                var connection = new SqlConnection(_connectionString);
                return connection;
            })
            .As<System.Data.IDbConnection>();

            //Func<IResponsibleSystemDbContext> f = () => new ResponsibleSystemDbContextFactory().CreateDbContext(_connectionString);
            //builder.RegisterInstance(f).As<Func<IResponsibleSystemDbContext>>();
        }
    }
}
