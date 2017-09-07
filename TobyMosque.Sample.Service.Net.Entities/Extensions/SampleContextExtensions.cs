using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Extensions
{
    public static class SampleContextExtensions
    {
        public static void Seed(this SampleContext db)
        {
            db.SeedTenants();
            db.SeedAuditTypes();
            db.SeedMovimentTypes();
            db.SeedAdminUser();
            db.SaveChanges();
        }

        private static void SeedTenants(this SampleContext db)
        {
            var tenants = new DataEntities.Tenant[]
            {
                new DataEntities.Tenant { TenantID = db.TenantID, Description = "Tenant 01" },
                new DataEntities.Tenant { TenantID = 2, Description = "Tenant 02" },
                new DataEntities.Tenant { TenantID = 3, Description = "Tenant 03" },
                new DataEntities.Tenant { TenantID = 4, Description = "Tenant 04" },
            };

            var tenantsId = tenants.Select(x => x.TenantID).ToList();
            var dbTenants = db.Tenants.Where(x => tenantsId.Contains(x.TenantID)).ToList();
            tenants = (
                from dsTenant in tenants
                join dbTenant in dbTenants on dsTenant.TenantID equals dbTenant.TenantID into left
                from dbTenant in left.DefaultIfEmpty()
                where dbTenant == null
                select dsTenant).ToArray();

            db.Tenants.AddRange(tenants);
        }

        private static void SeedMovimentTypes(this SampleContext db)
        {
            var enumType = typeof(DataEntities.Enums.MovimentType);
            var movimentTypesId = Enum.GetValues(enumType).Cast<DataEntities.Enums.MovimentType>().ToArray();
            var movimentTypes = movimentTypesId.Select(tipo => {
                var tipoMovimentacao = new DataEntities.Domain.MovimentType();
                tipoMovimentacao.MovimentTypeID = tipo;
                tipoMovimentacao.Description = Enum.GetName(enumType, tipo);
                return tipoMovimentacao;
            }).ToArray();

            var dbTiposMovimentacao = db.MovimentTypes.Where(x => movimentTypesId.Contains(x.MovimentTypeID)).ToList();
            movimentTypes = (
                from dsMovimentType in movimentTypes
                join dbMovimentType in dbTiposMovimentacao on dsMovimentType.MovimentTypeID equals dbMovimentType.MovimentTypeID into left
                from dbMovimentType in left.DefaultIfEmpty()
                where dbMovimentType == null
                select dsMovimentType).ToArray();

            db.MovimentTypes.AddRange(movimentTypes);
        }

        private static void SeedAuditTypes(this SampleContext db)
        {
            var enumType = typeof(AuditEntities.Enums.AuditType);
            var auditTypesId = Enum.GetValues(enumType).Cast<AuditEntities.Enums.AuditType>().ToArray();
            var auditTypes = auditTypesId.Select(tipo => {
                var tipoHistorico = new AuditEntities.Domain.AuditType();
                tipoHistorico.AuditTypeID = tipo;
                tipoHistorico.Description = Enum.GetName(enumType, tipo);
                return tipoHistorico;
            }).ToArray();

            var dbTiposHistorico = db.AuditTypes.Where(x => auditTypesId.Contains(x.AuditTypeID)).ToList();
            auditTypes = (
                from dsAuditType in auditTypes
                join dbAuditType in dbTiposHistorico on dsAuditType.AuditTypeID equals dbAuditType.AuditTypeID into left
                from dbAuditType in left.DefaultIfEmpty()
                where dbAuditType == null
                select dsAuditType).ToArray();

            db.AuditTypes.AddRange(auditTypes);
        }

        private static void SeedAdminUser(this SampleContext db)
        {
            var usuarioId = Guid.Parse("{30EA0242-4937-4C2D-8BE4-EA9FA4B2A97E}");
            var usuario = db.Users.Where(x => x.UserID == usuarioId).FirstOrDefault();
            if (usuario == null)
            {
                usuario = new DataEntities.User();
                usuario.UserID = usuarioId;
                usuario.Logon = "admin";
                usuario.RegisterPassword("H3ll0@W0rld");

                db.Users.Add(usuario);
            }
        }
    }
}
