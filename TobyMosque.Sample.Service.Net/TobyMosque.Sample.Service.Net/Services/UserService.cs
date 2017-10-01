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
    public static class UserService
    {
        public static async Task<DataResponse<UserModel>> Get()
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<UserModel>();
                response.ResponseCode = 200;
                response.Data = await db.GetUserModelBySessionId(db.SessionID.Value);
                return response;
            }
        }

        public static async Task<DataResponse<bool>> Logout()
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var sessao = await db.GetSession(db.SessionID.Value);
                sessao.IsActive = false;
                await db.SaveChangesAsync();

                var response = new DataResponse<bool>();
                response.ResponseCode = 200;
                response.Data = true;
                return response;
            }
        }        
    }
}
