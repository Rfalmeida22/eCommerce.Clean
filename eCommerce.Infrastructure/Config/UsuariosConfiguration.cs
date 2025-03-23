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
    public class UsuariosConfiguration : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.Property(e => e.Usuarios_Cod)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.IdBroker)
                .HasColumnType("int");

            builder.Property(e => e.IdLoja)
                .HasColumnType("int");

            builder.Property(e => e.IdVarejista)
                .HasColumnType("int");

            builder.Property(e => e.SenhaAnterior)
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            builder.Property(e => e.Usuarios_Ati)
                .ValueGeneratedOnAdd()
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(e => e.Usuarios_DatCad)
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime2")
                .HasDefaultValue(new DateTime(2022, 10, 30, 17, 25, 10, 612, DateTimeKind.Local).AddTicks(1808));

            builder.Property(e => e.Usuarios_Ema)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Usuarios_EmpPad)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasDefaultValue(1);

            builder.Property(e => e.Usuarios_Nom)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Usuarios_Sen)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            builder.Property(e => e.Usuarios_VisAutcom)
                .HasColumnType("bit");

            builder.Property(e => e.Usuarios_VisAutemp)
                .HasColumnType("bit");

            builder.HasKey(e => e.Usuarios_Cod);

            builder.ToTable("Usuarios");
        }
    }
}
