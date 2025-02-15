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
    public class LogImportacaoVarejoDetalheConfiguration : IEntityTypeConfiguration<LogImportacaoVarejoDetalhe>
    {
        public void Configure(EntityTypeBuilder<LogImportacaoVarejoDetalhe> builder)
        {
            builder.Property(e => e.IdDetalhe)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.Broker)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.CdCartao)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.CpfComprador)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.DataCancelamento)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.DataCriacao)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.DataValidade)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.DataVenda)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.IdLog)
                .HasColumnType("int");

            builder.Property(e => e.Loja)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.Observacao)
                .HasMaxLength(1255)
                .HasColumnType("nvarchar(1255)");

            builder.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Varejista)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(e => e.Vendedor)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.HasKey(e => e.IdDetalhe);

            builder.ToTable("LogImportacaoVarejoDetalhe");
        }
    }
}
