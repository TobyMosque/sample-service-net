using System;
using System.Collections.Generic;
using System.Text;

namespace TobyMosque.Sample.Service.Net.DataEntities
{
    public class Tenant
    {
        public int TenantID { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public ICollection<Moviment> Moviments { get; set; }
    }
}
