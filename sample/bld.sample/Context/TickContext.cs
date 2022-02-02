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
            var connection = GetConnectionStringFromKeyVault();
            optionsBuilder.UseSqlServer(connection);
        }

        internal DbSet<Tick> Ticks { get; set; }

        private static System.Data.SqlClient.SqlConnection GetConnectionStringFromKeyVault()
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            if (string.IsNullOrEmpty(keyVaultName))
            {
                throw new Exception("Kevault not configured; cannot continue");
            }
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            var azureCredential = new DefaultAzureCredential();
            var client = new SecretClient(new Uri(kvUri), azureCredential);
            var connectionString = client.GetSecretAsync("ConnectionString").GetAwaiter().GetResult().Value.Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("No connection string configured; cannot continue");
            }
            var conn = new System.Data.SqlClient.SqlConnection(connectionString);
            conn.AccessToken = azureCredential.GetToken(new Azure.Core.TokenRequestContext(new string[] { "https://database.windows.net" })).Token;
            return conn;
        }
    }
}
