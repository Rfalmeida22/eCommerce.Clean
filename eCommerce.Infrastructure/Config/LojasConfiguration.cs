using eCommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Config
{
    public class LojasConfiguration : IEntityTypeConfiguration<Lojas>
    {
        public void Configure(EntityTypeBuilder<Lojas> builder)
        {
            builder.Property(e => e.IdLoja)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.CdCnpj)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.CdLoja)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.IdLojista)
                .HasColumnType("int");

            builder.Property(e => e.IdVarejista)
                .HasColumnType("int");

            builder.Property(e => e.NmLoja)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.TxEndereco)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.HasKey(e => e.IdLoja);

            builder.ToTable("Lojas");
        }
    }
}
