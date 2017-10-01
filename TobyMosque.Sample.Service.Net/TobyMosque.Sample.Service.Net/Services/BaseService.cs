using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using TobyMosque.Sample.Service.Net.Models;

namespace TobyMosque.Sample.Service.Net.Services
{
    public static class BaseService
    {
        private static IConfigurationRoot Root { get; set; }
        static BaseService()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            BaseService.Root = builder.Build();
        }

        private static string Provider { get { return BaseService.Root["provider"]; } }
        private static string SqlServerConnection { get { return BaseService.Root["connectionStrings:sqlServer"]; } }
        private static string PostgreSqlConnection { get { return BaseService.Root["connectionStrings:postgreSql"]; } }

        internal static Func<Guid?> getSessionId = () => default(Guid?);
        internal static Func<int> getTenantId = () => default(int);

        public static async Task ConfigGetSessionId(Func<Guid?> getSessionId)
        {
            if (getSessionId == null)
                return;
            BaseService.getSessionId = getSessionId;
        }

        public static async Task ConfigGetTenantId(Func<int> getTenantId)
        {
            if (getTenantId == null)
                return;
            BaseService.getTenantId = getTenantId;
        }

        internal static SampleContext CreateSampleContext()
        {
            var sessionId = BaseService.getSessionId.Invoke();
            var tenantId = BaseService.getTenantId.Invoke();
            if (BaseService.Provider == "sqlServer")
            {
                var optionsBuilder = new DbContextOptionsBuilder<SqlServer.SampleContext>();
                optionsBuilder.UseSqlServer(BaseService.SqlServerConnection);
                return new SqlServer.SampleContext(optionsBuilder.Options, sessionId, tenantId);
            }
            if (BaseService.Provider == "postgreSql")
            {
                var optionsBuilder = new DbContextOptionsBuilder<Npgsql.SampleContext>();
                optionsBuilder.UseNpgsql(BaseService.PostgreSqlConnection);
                return new Npgsql.SampleContext(optionsBuilder.Options, sessionId, tenantId);
            }
            return null;
        }

        internal static DataResponse<T1> GetErrorsFromModel<T1, T2>(this DataResponse<T1> response, T2 model) where T2: IValidatableObject
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results);
            if (!isValid)
            {
                foreach (var result in results)
                {
                    response.AddError(result.ErrorMessage, result.MemberNames.ToArray());
                }
            }
            return response;
        }
    }
}
