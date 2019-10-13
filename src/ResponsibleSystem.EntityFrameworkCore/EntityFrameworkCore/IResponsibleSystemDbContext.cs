using Microsoft.EntityFrameworkCore;
using ResponsibleSystem.Logs;
using EFCore.BulkExtensions;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ResponsibleSystem.Entities;
using ResponsibleSystem.Migrations;

namespace ResponsibleSystem.EntityFrameworkCore
{
    public interface IResponsibleSystemDbContext : IDisposable
    {
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();

        void InsertBulk<TEntity>(IList<TEntity> entities, BulkConfig options = null, Action<decimal> progressCallback = null) where TEntity : class;
        void UpdateBulk<TEntity>(IList<TEntity> entities, BulkConfig options = null, Action<decimal> progressCallback = null) where TEntity : class;

        DbSet<Leather> Leathers { get; set; }
        DbSet<Farm> Farms { get; set; }
        DbSet<Inventory> Inventory { get; set; }
        DbSet<Production> Productions { get; set; }

        DbSet<Log> Logs { get; set; }
        DbSet<AppMigration> AppMigrations { get; set; }


        void SetModified<TEntity>(TEntity entity) where TEntity : class;
        void SetAdded<TEntity>(TEntity entity) where TEntity : class;
        void SetDeleted<TEntity>(TEntity entity) where TEntity : class;
    }
}
