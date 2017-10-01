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
    internal static class ResourceQueries
    {
        private static readonly Func<SampleContext, AsyncEnumerable<ResourceModel>> _GetResources = EF
            .CompileAsyncQuery((SampleContext db) => db.Resources
                .Select(r => new ResourceModel
                {
                    Description = r.Description,
                    Observation = r.Observation,
                    Quantity = r.Quantity,
                    ResourceID = r.ResourceID
                }));

        private static readonly Func<SampleContext, Guid, Task<Resource>> _GetResourceById = EF
            .CompileAsyncQuery((SampleContext db, Guid resourceId) => db.Resources.FirstOrDefault(r => r.ResourceID == resourceId));

        internal static async Task<List<ResourceModel>> GetResources(this SampleContext db)
        {
            return await _GetResources(db).ToListAsync();
        }

        internal static async Task<Resource> GettResourceById(this SampleContext db, Guid resourceId)
        {
            return await _GetResourceById(db, resourceId);
        }
    }
}
