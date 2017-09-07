using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    public class Resource : Entities.Resource
    {
        public ICollection<AuditEntities.Resource> AuditLog { get; set; }
    }
}
