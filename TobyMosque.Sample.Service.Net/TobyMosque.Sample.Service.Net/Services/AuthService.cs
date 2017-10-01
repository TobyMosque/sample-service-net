using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TobyMosque.Sample.Service.Net.Extensions;
using TobyMosque.Sample.Service.Net.Models;
using TobyMosque.Sample.Service.Net.Queries;

namespace TobyMosque.Sample.Service.Net.Services
{
    public static class AuthService
    { 
        public static async Task<DataResponse<List<DataEntities.Tenant>>> GetTenants()
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<List<DataEntities.Tenant>>();
                response.ResponseCode = 200;
                response.Data = await db.GetTenants();
                return response;
            }
        }

        public static async Task<DataResponse<string>> Login(LoginSignupModel model)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<string>();
                if (string.IsNullOrWhiteSpace(model.Logon) || string.IsNullOrWhiteSpace(model.Password))
                {
                    response.ResponseCode = 400;
                    response.Message = "Logon and Password can't be empty";
                    return response;
                }

                db.TenantID = model.TenantId;
                var user = await db.GetUserByLogon(model.Logon);
                if (user == null || !user.VerifyPassword(model.Password))
                {
                    response.ResponseCode = 400;
                    response.Message = "User not found or password is incorrect";
                    return response;
                }

                var sessao = user.CreateSession();
                await db.Sessions.AddAsync(sessao);
                await db.SaveChangesAsync();

                response.ResponseCode = 200;
                response.Data = Convert.ToBase64String(sessao.Token);
                return response;
            }
        }

        public static async Task<DataResponse<string>> SignUp(LoginSignupModel model)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<string>();
                if (string.IsNullOrWhiteSpace(model.Logon) || string.IsNullOrWhiteSpace(model.Password))
                {
                    response.ResponseCode = 400;
                    response.Message = "Logon and Password can't be empty";
                    return response;
                }

                var grupo = await db.GetTenantById(model.TenantId);
                if (grupo == null)
                {
                    response.ResponseCode = 400;
                    response.Message = "Tenant not found.";
                    return response;
                }

                db.TenantID = model.TenantId;

                var user = await db.GetUserByLogon(model.Logon);
                if (user != null)
                {
                    response.ResponseCode = 400;
                    response.Message = "Login Name already in use by another User.";
                    return response;
                }

                user = new DataEntities.User();
                user.UserID = Guid.NewGuid();
                user.Logon = model.Logon;
                user.TenantID = model.TenantId;
                user.RegisterPassword(model.Password);
                var sessao = user.CreateSession();

                await db.Users.AddAsync(user);
                await db.Sessions.AddAsync(sessao);
                await db.SaveChangesAsync();

                response.ResponseCode = 200;
                response.Data = Convert.ToBase64String(sessao.Token);
                return response;
            }
        }

        public static async Task<DataResponse<DataEntities.Session>> GetSessionByToken(byte[] token)
        {
            using (var db = BaseService.CreateSampleContext())
            {
                var response = new DataResponse<DataEntities.Session>();
                response.ResponseCode = 200;
                response.Data = await db.GetSessionByToken(token);
                return response;
            }
        }
    }
}
