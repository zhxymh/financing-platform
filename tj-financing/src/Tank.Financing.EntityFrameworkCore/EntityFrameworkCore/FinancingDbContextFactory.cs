using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Tank.Financing.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class FinancingDbContextFactory : IDesignTimeDbContextFactory<FinancingDbContext>
{
    public FinancingDbContext CreateDbContext(string[] args)
    {
        FinancingEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString("Default");
        var builder = new DbContextOptionsBuilder<FinancingDbContext>()
            .UseMySql(connectionString, ServerVersion.Create(8, 0, 28, ServerType.MySql));

        return new FinancingDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Tank.Financing.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
