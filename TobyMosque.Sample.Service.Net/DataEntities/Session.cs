using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    public class Session
    {
        public Guid SessionID { get; set; }
        public Guid UserID { get; set; }
        public byte[] Token { get; set; }
        public bool IsActive { get; set; }
        public int TenantID { get; set; }
        public DateTime CreationDate { get; set; }

        public User User { get; set; }
        public Tenant Tenant { get; set; }
    }
}
