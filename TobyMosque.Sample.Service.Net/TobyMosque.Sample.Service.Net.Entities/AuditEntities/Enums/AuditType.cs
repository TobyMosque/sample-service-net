using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.AuditEntities.Enums
{
    [DataContract(IsReference = true)]
    public enum AuditType : byte
    {
        [EnumMember]
        Invalid = 0,
        [EnumMember]
        Insert = 1,
        [EnumMember]
        Update = 2,
        [EnumMember]
        Delete = 3
    }
}
