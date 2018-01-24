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

                n.HasOne(c => c.Trade)
                 .WithMany(t => t.Contracts)
                 .OnDelete(DeleteBehavior.Restrict);
                
                n.Property(c => c.Price)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(c => c.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");
            });

        }
    }
}
