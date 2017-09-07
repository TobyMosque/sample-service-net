using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Entities
{
    public abstract class User : BaseEntity<AuditEntities.User>
    {
        public Guid UserID { get; set; }
        public string Logon { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
