using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.AuditEntities
{
    public class User : Entities.User, Entities.IAuditEntity
    {
        public Guid AuditID { get; set; }
        public Enums.AuditType AuditTypeID { get; set; }
        public DateTime AuditDate { get; set; }
        public Guid? SessionID { get; set; }

        public DataEntities.Session Session { get; set; }
        public DataEntities.User Entity { get; set; }
        public Domain.AuditType AuditType { get; set; }
    }
}
