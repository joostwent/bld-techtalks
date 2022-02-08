using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using bld.sample.Model;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bld.sample.Context
{
    internal class TickContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = GetConnection();
            optionsBuilder.UseSqlServer(connection);
        }

        internal DbSet<Tick> Ticks { get; set; }

        private static System.Data.SqlClient.SqlConnection GetConnection()
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            var conn = new System.Data.SqlClient.SqlConnection(connectionString);
            return conn;
        }
    }
}
