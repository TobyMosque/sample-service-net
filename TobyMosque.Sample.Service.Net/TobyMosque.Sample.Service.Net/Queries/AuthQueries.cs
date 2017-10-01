using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.DataEntities;

namespace TobyMosque.Sample.Service.Net.Queries
{
    internal static class AuthQueries
    {
        private static readonly Func<SampleContext, AsyncEnumerable<Tenant>> _GetTenants = EF
            .CompileAsyncQuery((SampleContext db) => db.Tenants);

        private static readonly Func<SampleContext, int, Task<Tenant>> _GetTenantById = EF
            .CompileAsyncQuery((SampleContext db, int tenantId) => db.Tenants
                .FirstOrDefault(u => u.TenantID == tenantId));

        private static readonly Func<SampleContext, string, Task<User>> _GetUserByLogon = EF
            .CompileAsyncQuery((SampleContext db, string logon) => db.Users
                .FirstOrDefault(u => u.Logon == logon));

        private static readonly Func<SampleContext, byte[], Task<Session>> _GetSessionByToken = EF
            .CompileAsyncQuery((SampleContext db, byte[] token) => db.Sessions
                .FirstOrDefault(s => s.Token == token && s.IsActive));

        internal static async Task<List<Tenant>> GetTenants(this SampleContext db)
        {
            return await _GetTenants(db).ToListAsync();
        }

        internal static async Task<User> GetUserByLogon(this SampleContext db, string logon)
        {
            return await _GetUserByLogon(db, logon);
        }

        internal static async Task<Tenant> GetTenantById(this SampleContext db, int tenantId)
        {
            return await _GetTenantById(db, tenantId);
        }

        internal static async Task<Session> GetSessionByToken(this SampleContext db, byte[] token)
        {
            return await _GetSessionByToken(db, token);
        }
    }
}
