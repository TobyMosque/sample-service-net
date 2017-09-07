using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    public class User : Entities.User
    {
        public ICollection<AuditEntities.User> AuditLog { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
