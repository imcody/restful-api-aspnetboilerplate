using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Logs;
using ResponsibleSystem.MultiTenancy;
using EFCore.BulkExtensions;
using System.Collections.Generic;
using System;
using ResponsibleSystem.Entities;
using ResponsibleSystem.Migrations;

namespace ResponsibleSystem.EntityFrameworkCore
{
    public class ResponsibleSystemDbContext : AbpZeroDbContext<Tenant, Role, User, ResponsibleSystemDbContext>, IResponsibleSystemDbContext
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Leather> Leathers { get; set; }
        public virtual DbSet<Farm> Farms { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<AppMigration> AppMigrations { get; set; }

        public ResponsibleSystemDbContext(DbContextOptions<ResponsibleSystemDbContext> options)
            : base(options)
        {
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            this.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void SetAdded<TEntity>(TEntity entity) where TEntity : class
        {
            this.Entry<TEntity>(entity).State = EntityState.Added;
        }

        public void SetDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            this.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public void InsertBulk<TEntity>(IList<TEntity> entities, BulkConfig options = null, Action<decimal> progressCallback = null) where TEntity : class
        {
            options = options ?? new BulkConfig();
            options.BulkCopyTimeout = 3600;
            this.BulkInsert(entities, options, progressCallback);
        }

        public void UpdateBulk<TEntity>(IList<TEntity> entities, BulkConfig options = null, Action<decimal> progressCallback = null) where TEntity : class
        {
            options = options ?? new BulkConfig();
            options.BulkCopyTimeout = 3600;
            this.BulkUpdate(entities, options, progressCallback);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leather>()
                .HasIndex(x => new {x.IdNo, x.PPNo})
                .IsUnique();

            modelBuilder.Entity<Leather>()
                .Property(x => x.IdNo).IsRequired();

            modelBuilder.Entity<Leather>()
                .Property(x => x.PPNo).IsRequired();

            modelBuilder.Entity<Leather>()
                .Property(x => x.Gender).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
