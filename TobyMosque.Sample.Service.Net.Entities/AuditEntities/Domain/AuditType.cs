using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.AuditEntities.Domain
{
    [DataContract(IsReference = true)]
    public class AuditType
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Enums.AuditType AuditTypeID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Description { get; set; }
    }
}
