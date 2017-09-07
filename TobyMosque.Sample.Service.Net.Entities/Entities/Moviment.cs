using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public abstract class Moviment : BaseEntity<AuditEntities.Moviment>
    {
        public Guid MovimentID { get; set; }
        public Guid ResourceID { get; set; }
        public Guid UserID { get; set; }
        public DataEntities.Enums.MovimentType MovimentTypeID { get; set; }
        public int Quantity { get; set; }

        public DataEntities.User User { get; set; }
        public DataEntities.Resource Resource { get; set; }
        public DataEntities.Domain.MovimentType MovimentType { get; set; }
    }
}
