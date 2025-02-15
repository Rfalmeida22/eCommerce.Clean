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
    public class HistoricosConfiguration : IEntityTypeConfiguration<Historicos>
    {
        public void Configure(EntityTypeBuilder<Historicos> builder)
        {
            builder.Property(e => e.Historicos_Cod)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.Historicos_Aca)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnType("nvarchar(1)");

            builder.Property(e => e.Historicos_Dat)
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime2")
                .HasDefaultValue(new DateTime(2022, 10, 30, 17, 25, 10, 457, DateTimeKind.Local).AddTicks(4796));

            builder.Property(e => e.Historicos_Det)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Historicos_Tab)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)");

            builder.Property(e => e.IdEmpresa)
                .HasColumnType("int");

            builder.Property(e => e.Usuarios_Cod)
                .HasColumnType("int");

            builder.HasKey(e => e.Historicos_Cod);

            builder.ToTable("Historicos");
        }
    }
}
