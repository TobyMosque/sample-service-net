using EntityFrameworkCore.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public abstract class BaseEntity<T> : IDataEntity<T> where T : class, IAuditEntity
    {
        public int TenantID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        public DataEntities.Tenant Tenant { get; set; }

        private static bool IsSimpleType(Type type)
        {
            var info = type.GetTypeInfo();
            return info.IsPrimitive || info.IsValueType || info.IsSealed;
        }

        private static T InsertAudit(SampleContext sampleContext, BaseEntity<T> entity, AuditEntities.Enums.AuditType auditTypeId)
        {
            var audit = (T)Activator.CreateInstance(typeof(T));
            var auditType = audit.GetType();
            var entityType = entity.GetType();

            var entityProps = entityType.GetProperties()
                .Where(x => IsSimpleType(x.PropertyType))
                .ToDictionary(x => x.Name, x => x);

            var auditProps = auditType.GetProperties()
                .Where(x => entityProps.ContainsKey(x.Name))
                .ToDictionary(x => x.Name, x => x);

            var props = new string[auditProps.Count];
            auditProps.Keys.CopyTo(props, 0);
            foreach (var prop in props)
            {
                var value = entityProps[prop].GetValue(entity);
                auditProps[prop].SetValue(audit, value);
            }

            audit.AuditDate = DateTime.UtcNow;
            audit.AuditID = Guid.NewGuid();
            audit.AuditTypeID = auditTypeId;
            audit.SessionID = sampleContext.SessionID;

            sampleContext.Add(audit);
            return audit;
        }

        static BaseEntity()
        {
            Triggers<BaseEntity<T>>.Inserting += entry =>
            {
                if (entry.Entity is IAuditEntity)
                    return;
                var sampleContext = entry.Context as SampleContext;
                entry.Entity.CreationDate = DateTime.Now;
                entry.Entity.TenantID = sampleContext.TenantID;
                entry.Entity.IsDeleted = false;
                InsertAudit(sampleContext, entry.Entity, AuditEntities.Enums.AuditType.Insert);
            };
            Triggers<BaseEntity<T>>.Updating += entry =>
            {
                if (entry.Entity is IAuditEntity)
                    return;
                var sampleContext = entry.Context as SampleContext;
                InsertAudit(sampleContext, entry.Entity, AuditEntities.Enums.AuditType.Update);
            };
            Triggers<BaseEntity<T>>.Deleting += entry =>
            {
                if (entry.Entity is IAuditEntity)
                    return;
                var sampleContext = entry.Context as SampleContext;
                entry.Entity.IsDeleted = true;
                InsertAudit(sampleContext, entry.Entity, AuditEntities.Enums.AuditType.Delete);
                entry.Cancel = true;
            };
        }
    }
}
