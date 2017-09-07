using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public interface IAuditEntity
    {
        Guid AuditID { get; set; }
        AuditEntities.Enums.AuditType AuditTypeID { get; set; }
        DateTime AuditDate { get; set; }
        Guid? SessionID { get; set; }

        DataEntities.Session Session { get; set; }
        AuditEntities.Domain.AuditType TipoHistorico { get; set; }
    }
}
