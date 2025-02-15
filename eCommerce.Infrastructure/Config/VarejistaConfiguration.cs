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
    public class VarejistaConfiguration : IEntityTypeConfiguration<Varejista>
    {
        public void Configure(EntityTypeBuilder<Varejista> builder)
        {
            builder.Property(e => e.IdVarejista)
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.CdBanner)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.CdCorFundo)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.CdVarejista)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.NmVarejista)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.TxLinkSite)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.HasKey(e => e.IdVarejista);

            builder.ToTable("Varejistas");
        }
    }
}
