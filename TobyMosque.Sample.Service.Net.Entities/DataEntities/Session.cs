using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    [DataContract(IsReference = true)]
    public class Session
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Guid SessionID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public Guid UserID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public byte[] Token { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public bool IsActive { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 5)]
        public int TenantID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 6)]
        public DateTime CreationDate { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 7)]
        public User User { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 8)]
        public Tenant Tenant { get; set; }
    }
}
