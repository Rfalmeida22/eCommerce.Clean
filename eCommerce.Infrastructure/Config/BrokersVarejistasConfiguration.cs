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
    public class BrokersVarejistasConfiguration : IEntityTypeConfiguration<Brokers_Varejistas>
    {
        public void Configure(EntityTypeBuilder<Brokers_Varejistas> builder)
        {
            builder.Property(e => e.IdSequencial)
               .ValueGeneratedOnAdd()
               .HasColumnType("int")
               .HasAnnotation("SqlServer:ValueGenerationStrategy",
                   SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.IdBroker)
                .HasColumnType("int");

            builder.Property(e => e.IdVarejista)
                .HasColumnType("int");

            builder.HasKey(e => e.IdSequencial);

            builder.ToTable("Brokers_Varejistas");
        }
    }
}
