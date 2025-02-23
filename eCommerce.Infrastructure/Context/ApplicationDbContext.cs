using eCommerce.Domain.Entities.Usuarios;
using eCommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using eCommerce.Infrastructure.Config;

namespace eCommerce.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {

        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString,
                    new Action<SqlServerDbContextOptionsBuilder>(options =>
                    {
                        options.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: new[] { 1205 }
                        );
                    })
                );
            }
        }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new BrokerConfiguration());

        }

        // DbSet para cada entidade
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Varejista> Varejistas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Lojas> Lojas { get; set; }
        public DbSet<Historicos> Historicos { get; set; }
        public DbSet<LogImportacaoVarejoDetalhe> LogImportacaoVarejoDetalhes { get; set; }

    }
}
