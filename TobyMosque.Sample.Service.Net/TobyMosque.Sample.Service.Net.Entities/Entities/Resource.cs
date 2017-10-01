using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    [DataContract(IsReference = true)]
    public abstract class Resource : BaseEntity<AuditEntities.Resource>
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Guid ResourceID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Description { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public string Observation { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public int Quantity { get; set; }
    }
}
