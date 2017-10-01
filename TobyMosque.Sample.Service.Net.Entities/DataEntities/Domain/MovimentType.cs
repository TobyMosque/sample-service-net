using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities.Domain
{
    [DataContract(IsReference = true)]
    public class MovimentType
    {
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Enums.MovimentType MovimentTypeID { get; set; }
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Description { get; set; }
    }
}
