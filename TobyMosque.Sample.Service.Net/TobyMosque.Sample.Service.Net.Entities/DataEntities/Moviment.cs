using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    [DataContract(IsReference = true)]
    public class Moviment : Entities.Moviment
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public ICollection<AuditEntities.Moviment> AuditLog { get; set; }
    }
}
