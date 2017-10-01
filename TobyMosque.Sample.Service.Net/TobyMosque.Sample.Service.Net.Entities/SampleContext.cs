using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.Entities;

namespace TobyMosque.Sample.Service.Net
{
    public abstract class SampleContext : DbContext
    {
        private string _tenantId;

        public Guid? SessionID { get; set; }
        public int TenantID
        {
            get { return Int32.Parse(_tenantId); }
            set { _tenantId = value.ToString(); }
        }

        public SampleContext() : base()
        {

        }

        public SampleContext(Guid? sessionID, int tenantID) : base()
        {
            this.SessionID = sessionID;
            this.TenantID = tenantID;
        }

        public SampleContext(DbContextOptions options) : base(options)
        {

        }

        public SampleContext(DbContextOptions options, Guid? sessionID, int tenantID) : base(options)
        {
            this.SessionID = sessionID;
            this.TenantID = tenantID;
        }

        public override Int32 SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        }

        public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        }

        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }

        public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<DataEntities.Tenant> Tenants { get; set; }
        public DbSet<DataEntities.Session> Sessions { get; set; }
        public DbSet<DataEntities.User> Users { get; set; }
        public DbSet<DataEntities.Resource> Resources { get; set; }
        public DbSet<DataEntities.Moviment> Moviments { get; set; }
        public DbSet<DataEntities.Domain.MovimentType> MovimentTypes { get; set; }

        public DbSet<AuditEntities.User> AuditLogUsers { get; set; }
        public DbSet<AuditEntities.Resource> AuditLogResources { get; set; }
        public DbSet<AuditEntities.Moviment> AuditLogMoviments { get; set; }
        public DbSet<AuditEntities.Domain.AuditType> AuditTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataEntities.Domain.MovimentType>(entity =>
            {
                entity.ToTable("MovimentTypes", "domain");
                entity.HasKey(x => x.MovimentTypeID);
                entity.Property(x => x.Description).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<AuditEntities.Domain.AuditType>(entity =>
            {
                entity.ToTable("AuditTypes", "domain");
                entity.HasKey(x => x.AuditTypeID);
                entity.Property(x => x.Description).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<DataEntities.Tenant>(entity =>
            {
                entity.ToTable("Tenants", "entity");
                entity.HasKey(x => x.TenantID);
                entity.Property(x => x.TenantID).ValueGeneratedNever();

                entity.HasIndex(x => x.Description).IsUnique();
                entity.Property(x => x.Description).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<DataEntities.User>(entity =>
            {
                entity.ToTable("Users", "entity");
                entity.HasKey(x => x.UserID).ForSqlServerIsClustered(false); ;
                entity.HasIndex(x => new { x.TenantID, x.Logon }).IsUnique();

                entity.Property(x => x.Logon).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Password).HasMaxLength(64).IsRequired();
                entity.Property(x => x.Salt).HasMaxLength(16).IsRequired();

                this.ModelDataEntity<DataEntities.User, AuditEntities.User>(entity, x => x.Users);
            });

            modelBuilder.Entity<AuditEntities.User>(entity =>
            {
                entity.ToTable("Users", "audit");
                entity.HasKey(x => x.AuditID).ForSqlServerIsClustered(false); ;

                entity.HasOne(x => x.Entity).WithMany(x => x.AuditLog).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => x.UserID);

                entity.Property(x => x.Logon).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Password).HasMaxLength(64).IsRequired();
                entity.Property(x => x.Salt).HasMaxLength(16).IsRequired();

                this.ModelDataEntity<AuditEntities.User, AuditEntities.User>(entity, null);
                this.ModelAuditEntity<AuditEntities.User>(entity);
            });

            modelBuilder.Entity<DataEntities.Resource>(entity =>
            {
                entity.ToTable("Resources", "entity");
                entity.HasKey(x => x.ResourceID).ForSqlServerIsClustered(false); ;
                entity.HasIndex(x => new { x.TenantID, x.Description }).IsUnique();

                entity.Property(x => x.Description).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Observation).HasMaxLength(250);

                this.ModelDataEntity<DataEntities.Resource, AuditEntities.Resource>(entity, x => x.Resources);
            });

            modelBuilder.Entity<AuditEntities.Resource>(entity =>
            {
                entity.ToTable("Resources", "audit");
                entity.HasKey(x => x.AuditID).ForSqlServerIsClustered(false); ;

                entity.HasOne(x => x.Entity).WithMany(x => x.AuditLog).HasForeignKey(x => x.ResourceID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => x.ResourceID);

                entity.Property(x => x.Description).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Observation).HasMaxLength(250);

                this.ModelDataEntity<AuditEntities.Resource, AuditEntities.Resource>(entity, null);
                this.ModelAuditEntity<AuditEntities.Resource>(entity);
            });

            modelBuilder.Entity<DataEntities.Moviment>(entity =>
            {
                entity.ToTable("Moviments", "entity");
                entity.HasKey(x => x.MovimentID).ForSqlServerIsClustered(false); ;

                entity.HasOne(x => x.Resource).WithMany(x => x.Moviments).HasForeignKey(x => x.ResourceID);
                entity.HasIndex(x => x.ResourceID);

                entity.HasOne(x => x.User).WithMany(x => x.Moviments).HasForeignKey(x => x.UserID);
                entity.HasIndex(x => x.UserID);

                entity.HasOne(x => x.MovimentType).WithMany(navigationExpression: null).HasForeignKey(x => x.MovimentTypeID);
                entity.HasIndex(x => x.MovimentTypeID);

                this.ModelDataEntity<DataEntities.Moviment, AuditEntities.Moviment>(entity, x => x.Moviments);
            });

            modelBuilder.Entity<AuditEntities.Moviment>(entity =>
            {
                entity.ToTable("Moviments", "audit");
                entity.HasKey(x => x.AuditID).ForSqlServerIsClustered(false);

                entity.HasOne(x => x.Resource).WithMany(navigationExpression: null).HasForeignKey(x => x.ResourceID);
                entity.HasIndex(x => x.ResourceID);

                entity.HasOne(x => x.User).WithMany(navigationExpression: null).HasForeignKey(x => x.UserID);
                entity.HasIndex(x => x.UserID);

                entity.HasOne(x => x.MovimentType).WithMany(navigationExpression: null).HasForeignKey(x => x.MovimentTypeID);
                entity.HasIndex(x => x.MovimentTypeID);

                entity.HasOne(x => x.Entity).WithMany(x => x.AuditLog).HasForeignKey(x => x.MovimentID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => x.MovimentID);

                this.ModelDataEntity<AuditEntities.Moviment, AuditEntities.Moviment>(entity, null);
                this.ModelAuditEntity<AuditEntities.Moviment>(entity);
            });

            modelBuilder.Entity<DataEntities.Session>(entity =>
            {
                entity.ToTable("Sessions", "entity");
                entity.HasKey(x => x.SessionID).ForSqlServerIsClustered(false);

                entity.HasOne(x => x.Tenant).WithMany(navigationExpression: null).HasForeignKey(x => x.TenantID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => new { x.TenantID, x.CreationDate }).IsUnique().ForSqlServerIsClustered();

                entity.HasOne(x => x.User).WithMany(x => x.Sessions).HasForeignKey(x => x.UserID);
                entity.HasIndex(x => x.UserID);

                entity.Property(x => x.Token).HasMaxLength(64);
                entity.HasIndex(x => x.Token).IsUnique();
            });
        }

        private void ModelDataEntity<T, T2>(EntityTypeBuilder<T> entity, Expression<Func<DataEntities.Tenant, IEnumerable<T>>> navigationExpression)
            where T : BaseEntity<T2>
            where T2 : class, IAuditEntity
        {
            entity.HasQueryFilter(x => !x.IsDeleted && x.TenantID.ToString() == _tenantId);
            entity.HasOne(x => x.Tenant).WithMany(navigationExpression).HasForeignKey(x => x.TenantID).OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.IsDeleted).IsRequired();

            var isHistorico = typeof(IAuditEntity).IsAssignableFrom(typeof(T));
            entity.HasIndex(new string[] { "TenantID", isHistorico ? "AuditDate" : "CreationDate" }).IsUnique().ForSqlServerIsClustered();
            entity.HasIndex(x => x.IsDeleted);
        }

        private void ModelAuditEntity<T>(EntityTypeBuilder<T> entity) where T : class, IAuditEntity
        {
            entity.HasOne(x => x.Session).WithMany(navigationExpression: null).HasForeignKey(x => x.SessionID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.AuditType).WithMany(navigationExpression: null).HasForeignKey(x => x.AuditTypeID).OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => x.SessionID);
            entity.HasIndex(x => x.AuditTypeID);
        }
    }
}
