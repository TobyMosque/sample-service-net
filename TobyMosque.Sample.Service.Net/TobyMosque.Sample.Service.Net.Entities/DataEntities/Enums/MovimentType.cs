using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities.Enums
{
    [DataContract(IsReference = true)]
    public enum MovimentType : byte
    {
        [EnumMember]
        Invalid = 0,
        [EnumMember]
        In = 1,
        [EnumMember]
        Out = 2
    }
}
