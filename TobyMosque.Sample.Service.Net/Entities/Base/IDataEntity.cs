using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public interface IDataEntity<T> where T : IAuditEntity
    {
        int TenantID { get; set; }
        DateTime CreationDate { get; set; }
        bool IsDeleted { get; set; }

        DataEntities.Tenant Tenant { get; set; }
    }
}
