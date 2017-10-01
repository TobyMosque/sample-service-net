using System;
using System.Runtime.Serialization;

namespace TobyMosque.Sample.Service.Net.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Order = 1)]
        public Guid UserID { get; set; }

        [DataMember(Order = 2)]
        public string Logon { get; set; }
    }
}
