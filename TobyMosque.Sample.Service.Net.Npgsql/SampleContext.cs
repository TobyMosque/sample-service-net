using Microsoft.EntityFrameworkCore;
using System;

namespace TobyMosque.Sample.Service.Net.Npgsql
{
    public class SampleContext : Net.SampleContext
    {
        public SampleContext() : base() { }
        public SampleContext(Guid? sessionId, int tenantId) : base(sessionId, tenantId) { }
        public SampleContext(DbContextOptions options) : base(options) { }
        public SampleContext(DbContextOptions options, Guid? sessionId, int tenantId) : base(options, sessionId, tenantId) { }
    }
}
