using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TobyMosque.Sample.Service.Net
{
    public class SampleContext : DbContext
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
        }
    }
}
