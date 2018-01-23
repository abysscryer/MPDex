using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Data.Mappers
{
    public class FeeMap
    {
        public FeeMap(ModelBuilder builder)
        {
            builder.Entity<Fee>(n => {

                n.HasOne(f => f.Coin)
                 .WithMany(c => c.Fees)
                 .OnDelete(DeleteBehavior.Restrict);

                n.Property(c => c.Id)
                 .ValueGeneratedNever();

                n.Property(f => f.Percent)
                 .IsRequired()
                 .HasColumnType("decimal(3, 3)");

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");
            });
        }
    }
}
