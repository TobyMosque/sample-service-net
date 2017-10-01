using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    [DataContract(IsReference = true)]
    public abstract class Moviment : BaseEntity<AuditEntities.Moviment>
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Guid MovimentID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public Guid ResourceID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public Guid UserID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public DataEntities.Enums.MovimentType MovimentTypeID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 5)]
        public int Quantity { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 6)]
        public DataEntities.User User { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 7)]
        public DataEntities.Resource Resource { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 8)]
        public DataEntities.Domain.MovimentType MovimentType { get; set; }
    }
}
