using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.DataEntities;
using TobyMosque.Sample.Service.Net.Models;

namespace TobyMosque.Sample.Service.Net.Queries
{
    internal static class MovimentQueries
    {
        private static readonly Func<SampleContext, Guid, Guid, AsyncEnumerable<MovimentModel>> _GetMovimentsByResourceId = EF
           .CompileAsyncQuery((SampleContext db, Guid resourceId, Guid userId) => db.Moviments
               .Include(r => r.User)
               .Include(r => r.MovimentType)
               .Where(r => r.ResourceID == resourceId)
               .Select(m => new MovimentModel
               {
                   MovimentID = m.MovimentID,
                   ResourceID = m.ResourceID,
                   UserID = m.UserID,
                   MovimentTypeID = m.MovimentTypeID,
                   Quantity = m.Quantity,
                   UserNome = m.User.Logon,
                   IsOwner = m.UserID == userId
               }));

        private static readonly Func<SampleContext, AsyncEnumerable<DataEntities.Domain.MovimentType>> _GetMovimentTypes = EF
            .CompileAsyncQuery((SampleContext db) => db.MovimentTypes);

        private static readonly Func<SampleContext, Guid, Task<bool>> _GetResourceExists = EF
            .CompileAsyncQuery((SampleContext db, Guid resourceId) => db.Resources
                .Any(x => x.ResourceID == resourceId));

        private static readonly Func<SampleContext, Guid, Task<Resource>> _GetResourceById = EF
            .CompileAsyncQuery((SampleContext db, Guid resourceId) => db.Resources.FirstOrDefault(r => r.ResourceID == resourceId));

        private static readonly Func<SampleContext, Guid, Task<ResourceModel>> _GetResourceModelById = EF
            .CompileAsyncQuery((SampleContext db, Guid resourceId) => db.Resources
                .Where(r => r.ResourceID == resourceId)
                .Select(r => new ResourceModel
                {
                    Description = r.Description,
                    Observation = r.Observation,
                    Quantity = r.Quantity,
                    ResourceID = r.ResourceID
                }).FirstOrDefault());

        private static readonly Func<SampleContext, Guid, Task<Moviment>> _GetMovimentById = EF
            .CompileAsyncQuery((SampleContext db, Guid movimentId) => db.Moviments
                .Include(m => m.Resource)
                .FirstOrDefault(m => m.MovimentID == movimentId));

        internal static async Task<List<DataEntities.Domain.MovimentType>> GetMovimentTypes(this SampleContext db)
        {
            return await _GetMovimentTypes(db).ToListAsync();
        }

        internal static async Task<List<MovimentModel>> GetMovimentsByResourceId(this SampleContext db, Guid resourceId, Guid userId)
        {
            return await _GetMovimentsByResourceId(db, resourceId, userId).ToListAsync();
        }

        internal static async Task<bool> GetResourceExists(this SampleContext db, Guid resourceId)
        {
            return await _GetResourceExists(db, resourceId);
        }

        internal static async Task<Resource> GetResourceById(this SampleContext db, Guid resourceId)
        {
            return await _GetResourceById(db, resourceId);
        }

        internal static async Task<ResourceModel> GetResourceModelById(this SampleContext db, Guid resourceId)
        {
            return await _GetResourceModelById(db, resourceId);
        }

        internal static async Task<Moviment> GetMovimentById(this SampleContext db, Guid movimentId)
        {
            return await _GetMovimentById(db, movimentId);
        }
    }
}
