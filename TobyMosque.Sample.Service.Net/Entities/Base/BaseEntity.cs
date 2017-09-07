using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public abstract class BaseEntity<T> : IDataEntity<T> where T : class, IAuditEntity
    {
        public int TenantID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        public DataEntities.Tenant Tenant { get; set; }
    }
}
