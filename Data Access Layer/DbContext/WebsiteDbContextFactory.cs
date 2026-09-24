using System;
using System.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VelascoPersonalWebsite_IPT.DataAccess.DbContext
{
    public class WebsiteDbContextFactory : IDesignTimeDbContextFactory<WebsiteDbContext>
    {
        public static WebsiteDbContext Create()
        {
            return CreateFromEnvironment();
        }

        WebsiteDbContext IDesignTimeDbContextFactory<WebsiteDbContext>.CreateDbContext(string[] args)
        {
            return CreateFromEnvironment();
        }

        private static WebsiteDbContext CreateFromEnvironment()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WebsiteDatabase"]?.ConnectionString;
            
           
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "A database connection string is required. Configure WEBSITE_DB_CONNECTION_STRING or the WebsiteDatabase connection string.");
            }

            var options = new DbContextOptionsBuilder<WebsiteDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new WebsiteDbContext(options);
        }
    }
}
