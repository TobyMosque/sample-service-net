using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.Extensions;
using TobyMosque.Sample.Service.Net.Models;
using TobyMosque.Sample.Service.Net.Queries;

namespace TobyMosque.Sample.Service.Net.Services
{
    public static class MovimentService
    {
        public static async Task<DataResponse<UserMovimentsByTenantModel>> Get(Guid resourceId)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<UserMovimentsByTenantModel>();
                var resourceExists = await db.GetResourceExists(resourceId);
                if (!resourceExists)
                {
                    response.ResponseCode = 404;
                    response.Message = "Resource not found";
                    return response;
                }

                var user = await db.GetUserModelBySessionId(db.SessionID.Value);
                var resource = await db.GetResourceModelById(resourceId);
                var moviments = await db.GetMovimentsByResourceId(resourceId, user.UserID);
                var movimentTypes = await db.GetMovimentTypes();

                response.ResponseCode = 200;
                response.Data = new UserMovimentsByTenantModel
                {
                    User = user,
                    Resource = resource,
                    MovimentTypes = movimentTypes,
                    Moviments = moviments
                };
                return response;
            } 
        }

        public static async Task<DataResponse<MovimentStockModel>> Post(MovimentModel model)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<MovimentStockModel>();
                var resource = await db.GetResourceById(model.ResourceID);
                if (resource == null)
                {
                    response.AddError("ResourceID", "Resource not found");
                }
                else if (model.MovimentTypeID == DataEntities.Enums.MovimentType.Out && model.Quantity > resource.Quantity)
                {
                    response.AddError("Quantity", "It's not possible to carry out a withdrawal, since the current stock is insufficient");
                }

                if (response.Errors.Count > 0)
                {
                    response.ResponseCode = 400;
                    return response;
                }

                var usuario = await db.GetUserModelBySessionId(db.SessionID.Value);
                switch (model.MovimentTypeID)
                {
                    case DataEntities.Enums.MovimentType.In:
                        resource.Quantity += model.Quantity;
                        break;
                    case DataEntities.Enums.MovimentType.Out:
                        resource.Quantity -= model.Quantity;
                        break;
                }

                var moviment = new DataEntities.Moviment();
                moviment.MovimentID = Guid.NewGuid();
                moviment.ResourceID = resource.ResourceID;
                moviment.UserID = usuario.UserID;
                moviment.MovimentTypeID = model.MovimentTypeID;
                moviment.Quantity = model.Quantity;

                await db.Moviments.AddAsync(moviment);
                await db.SaveChangesAsync();

                response.ResponseCode = 200;
                response.Data = new MovimentStockModel
                {
                    MovimentId = moviment.MovimentID,
                    NewResourceStock = resource.Quantity
                };
                return response;
            }
        }
    }
}
