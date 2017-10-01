using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.DataEntities;
using TobyMosque.Sample.Service.Net.Models;

namespace TobyMosque.Sample.Service.Net.Queries
{
    internal static class UserQueries
    {
        private static readonly Func<SampleContext, Guid, Task<Session>> _GetSession = EF
           .CompileAsyncQuery((SampleContext db, Guid sessionId) => db.Sessions
               .Include(s => s.User)
               .FirstOrDefault(x => x.SessionID == sessionId));

        private static readonly Func<SampleContext, Guid, Task<UserModel>> _GetUserModelBySessionId = EF
            .CompileAsyncQuery((SampleContext db, Guid sessionId) => db.Sessions
                .Include(s => s.User)
                .Select(x => new UserModel { UserID = x.UserID, Logon = x.User.Logon })
                .FirstOrDefault());

        internal static async Task<Session> GetSession(this SampleContext db, Guid sessionId)
        {
            return await _GetSession(db, sessionId);
        }

        internal static async Task<UserModel> GetUserModelBySessionId(this SampleContext db, Guid sessionId)
        {
            return await _GetUserModelBySessionId(db, sessionId);
        }
    }
}
