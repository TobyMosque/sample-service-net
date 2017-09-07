using Microsoft.EntityFrameworkCore;
using System;

namespace TobyMosque.Sample.Service.Net.SqlServer
{
    public class SampleContext : Net.SampleContext
    {
        public SampleContext() : base() { }
        public SampleContext(Guid? sessaoID, int grupoID) : base(sessaoID, grupoID) { }
        public SampleContext(DbContextOptions options) : base(options) { }
        public SampleContext(DbContextOptions options, Guid? sessaoID, int grupoID) : base(options, sessaoID, grupoID) { }
    }
}
