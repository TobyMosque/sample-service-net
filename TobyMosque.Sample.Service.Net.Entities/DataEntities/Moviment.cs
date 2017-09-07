using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    public class Moviment : Entities.Moviment
    {
        public ICollection<AuditEntities.Moviment> AuditLog { get; set; }
    }
}
