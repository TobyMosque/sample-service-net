using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    [DataContract(IsReference = true)]
    public class Resource : Entities.Resource
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public ICollection<AuditEntities.Resource> AuditLog { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public ICollection<Moviment> Moviments { get; set; }
    }
}
