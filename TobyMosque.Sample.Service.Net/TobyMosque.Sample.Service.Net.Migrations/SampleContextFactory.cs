﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace TobyMosque.Sample.Service.Net.Migrations
{
    internal static class Configuration
    {
        private static IConfigurationRoot Root { get; set; }
        static Configuration()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json");
            Configuration.Root = builder.Build();
        }

        public static Guid? SessionID
        {
            get
            {
                var config = Configuration.Root["sessionId"];
                if (string.IsNullOrWhiteSpace(config))
                    return default(Guid?);
                return Guid.Parse(config);
            }
        }

        public static int TenantID { get { return Int32.Parse(Configuration.Root["tenantID"]); } }

        public static string SqlServerConnection { get { return Configuration.Root["connectionStrings:sqlServer"]; } }
        public static string PostgreSqlConnection { get { return Configuration.Root["connectionStrings:postgreSql"]; } }
    }

    public class SampleContextFactory : IDesignTimeDbContextFactory<Npgsql.SampleContext>
    {
        public Npgsql.SampleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
            optionsBuilder.UseNpgsql(Configuration.PostgreSqlConnection);
            return new Npgsql.SampleContext(optionsBuilder.Options, Configuration.SessionID, Configuration.TenantID);
        }
    }

    //public class SampleContextFactory : IDesignTimeDbContextFactory<SqlServer.SampleContext>
    //{
    //    public SqlServer.SampleContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
    //        optionsBuilder.UseSqlServer(Configuration.SqlServerConnection);
    //        return new SqlServer.SampleContext(optionsBuilder.Options, Configuration.SessionID, Configuration.TenantID);
    //    }
    //}
}
