using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.Extensions;
using TobyMosque.Sample.Service.Net.Models;
using TobyMosque.Sample.Service.Net.Queries;

namespace TobyMosque.Sample.Service.Net.Services
{
    public static class ResourceService
    {
        public static async Task<DataResponse<List<ResourceModel>>> Get()
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<List<ResourceModel>>();
                response.ResponseCode = 200;
                response.Data = await db.GetResources();
                return response;
            }
        }

        public static async Task<DataResponse<Guid>> Post(ResourceModel model)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<Guid>();
                var erros = response.GetErrorsFromModel(model).GetMessageFromErrors();
                if (erros != string.Empty)
                {                    
                    response.Message = erros;
                    response.ResponseCode = 400;
                    return response;
                }

                model.ResourceID = Guid.NewGuid();
                var sessao = await db.GetSession(db.SessionID.Value);

                var resource = new DataEntities.Resource();
                resource.ResourceID = model.ResourceID.Value;
                resource.Description = model.Description;
                resource.Observation = model.Observation;
                resource.Quantity = model.Quantity;

                var moviment = new DataEntities.Moviment();
                moviment.MovimentID = Guid.NewGuid();
                moviment.ResourceID = model.ResourceID.Value;
                moviment.UserID = sessao.UserID;
                moviment.MovimentTypeID = DataEntities.Enums.MovimentType.In;
                moviment.Quantity = model.Quantity;

                await db.Resources.AddAsync(resource);
                await db.Moviments.AddAsync(moviment);
                await db.SaveChangesAsync();

                response.Data = resource.ResourceID;
                response.ResponseCode = 200;
                return response;
            }
        }

        public static async Task<DataResponse<Guid>> Put(ResourceModel model)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<Guid>();
                var resource = db.Resources.Find(model.ResourceID);
                if (resource == null)
                    response.AddError("ResourceID", "Resource not found");

                var erros = response.GetErrorsFromModel(model).GetMessageFromErrors();
                if (erros != string.Empty)
                {
                    response.Message = erros;
                    response.ResponseCode = 400;
                    return response;
                }

                var session = await db.Sessions.Include(x => x.User)
                    .FirstOrDefaultAsync(u => u.SessionID == db.SessionID);

                var diff = model.Quantity - resource.Quantity;
                resource.Description = model.Description;
                resource.Observation = model.Observation;
                resource.Quantity = model.Quantity;

                if (diff != 0)
                {
                    var moviment = new DataEntities.Moviment();
                    moviment.MovimentID = Guid.NewGuid();
                    moviment.ResourceID = model.ResourceID.Value;
                    moviment.UserID = session.UserID;
                    moviment.MovimentTypeID = diff > 0 ? DataEntities.Enums.MovimentType.In : DataEntities.Enums.MovimentType.Out;
                    moviment.Quantity = Math.Abs(diff);
                    await db.Moviments.AddAsync(moviment);
                }
                await db.SaveChangesAsync();

                response.Data = resource.ResourceID;
                response.ResponseCode = 200;
                return response;
            }
        }

        public static async Task<DataResponse<Guid>> Delete(Guid resourceId)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<Guid>();
                var resource = db.Resources.Find(resourceId);
                if (resource == null)
                    response.AddError("ResourceID", "Resource not found");

                var erros = response.GetMessageFromErrors();
                if (erros != string.Empty)
                {
                    response.Message = erros;
                    response.ResponseCode = 400;
                    return response;
                }

                db.Resources.Remove(resource);
                await db.SaveChangesAsync();

                response.Data = resource.ResourceID;
                response.ResponseCode = 200;
                return response;
            }
        }
    }
}
