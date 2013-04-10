using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using JabbR.Models;
using JabbR.Models.Migrations;

namespace JabbR
{
    public partial class Startup
    {
        private const string SqlClient = "System.Data.SqlClient";

        private static void DoMigrations()
        {
            // Get the Jabbr connection string
            /*
            var connectionString = ConfigurationManager.ConnectionStrings["Jabbr"];

            if (String.IsNullOrEmpty(connectionString.ProviderName) ||
                !connectionString.ProviderName.Equals(SqlClient, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            */

            using (var connection = System.Data.Entity.Database.DefaultConnectionFactory.CreateConnection("Jabbr"))
                if (!(connection is System.Data.SqlClient.SqlConnection))
                    return;

            Database.SetInitializer<JabbrContext>(null);

            // Only run migrations for SQL server (Sql ce not supported as yet)
            var settings = new MigrationsConfiguration();
            var migrator = new DbMigrator(settings);
            migrator.Update();
        }
    }
}