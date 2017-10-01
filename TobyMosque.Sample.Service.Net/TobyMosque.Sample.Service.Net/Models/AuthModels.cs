using System.Runtime.Serialization;

namespace TobyMosque.Sample.Service.Net.Models
{
    [DataContract]
    public class LoginSignupModel
    {
        [DataMember(Order = 1)]
        public int TenantId { get; set; }
        [DataMember(Order = 2)]
        public string Logon { get; set; }
        [DataMember(Order = 3)]
        public string Password { get; set; }
    }
}
