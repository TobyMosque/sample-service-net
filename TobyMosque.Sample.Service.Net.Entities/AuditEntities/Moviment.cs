using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.AuditEntities
{
    [DataContract(IsReference = true)]
    public class Moviment : Entities.Moviment, Entities.IAuditEntity
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Guid AuditID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public Enums.AuditType AuditTypeID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public DateTime AuditDate { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public Guid? SessionID { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 5)]
        public DataEntities.Session Session { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 6)]
        public DataEntities.Moviment Entity { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 7)]
        public Domain.AuditType AuditType { get; set; }
    }
}
