using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    [DataContract(IsReference = true)]
    public class User : Entities.User
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public ICollection<AuditEntities.User> AuditLog { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public ICollection<Moviment> Moviments { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public ICollection<Session> Sessions { get; set; }
    }
}
