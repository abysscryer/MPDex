using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Data.Mappers
{
    public class ContractMap
    {
        public ContractMap(ModelBuilder builder)
        {
            builder.Entity<Contract>(n => {
                n.HasKey(c => c.Id);

                n.HasOne(c => c.Trade)
                 .WithMany(t => t.Contracts)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasMany(c => c.Orders)
                 .WithOne(o => o.Contract)
                 .IsRequired();

                n.Property(b => b.Id)
                    .HasDefaultValueSql("newid()");

                n.Property(c => c.Price)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(c => c.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");
            });

        }
    }
}
