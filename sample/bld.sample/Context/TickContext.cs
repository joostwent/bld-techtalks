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
            string connectionString = GetConnectionStringFromKeyVault();
            optionsBuilder.UseSqlServer(connectionString);
        }

        internal DbSet<Tick> Ticks { get; set; }

        private static string GetConnectionStringFromKeyVault()
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            if (string.IsNullOrEmpty(keyVaultName))
            {
                throw new Exception("Kevault not configured; cannot continue");
            }
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var connectionString = client.GetSecretAsync("ConnectionString").GetAwaiter().GetResult().Value.Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("No connection string configured; cannot continue");
            }

            return connectionString;
        }
    }
}
