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

namespace eCommerce.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost;Database=eCommerce;User ID=sa;Password=MinhaSenha123;",
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

        // DbSet para cada entidade
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Varejista> Varejistas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Lojas> Lojas { get; set; }
        public DbSet<Historicos> Historicos { get; set; }
        public DbSet<LogImportacaoVarejoDetalhe> LogImportacaoVarejoDetalhes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


        
    }
}
