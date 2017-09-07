using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public abstract class Resource : BaseEntity<AuditEntities.Resource>
    {
        public Guid ResourceID { get; set; }
        public string Description { get; set; }
        public string Observation { get; set; }
        public int Quantity { get; set; }
    }
}
