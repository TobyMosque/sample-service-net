using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    [DataContract(IsReference = true)]
    public class Tenant
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public int TenantID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Description { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public ICollection<User> Users { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public ICollection<Resource> Resources { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 5)]
        public ICollection<Moviment> Moviments { get; set; }
    }
}
