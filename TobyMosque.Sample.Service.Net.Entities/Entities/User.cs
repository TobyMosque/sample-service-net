using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    [DataContract(IsReference = true)]
    public abstract class User : BaseEntity<AuditEntities.User>
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Guid UserID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Logon { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public byte[] Password { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public byte[] Salt { get; set; }
    }
}
