using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.AuditEntities
{
    public class Resource : Entities.Resource, Entities.IAuditEntity
    {
        public Guid AuditID { get; set; }
        public Enums.AuditType AuditTypeID { get; set; }
        public DateTime AuditDate { get; set; }
        public Guid? SessionID { get; set; }

        public DataEntities.Session Session { get; set; }
        public DataEntities.Resource Entity { get; set; }
        public Domain.AuditType AuditType { get; set; }
    }
}
