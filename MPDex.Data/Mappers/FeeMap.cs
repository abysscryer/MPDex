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
                //primary key
                n.HasKey(f => f.Id);
                //foreign key Coin
                n.HasOne(f => f.Coin)
                 .WithMany(c => c.Fees)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
                
                n.HasMany(f => f.Statements)
                 .WithOne(s => s.Fee);

                n.Property(c => c.Id)
                 .UseSqlServerIdentityColumn();

                n.Property(f => f.Percent)
                 .IsRequired().HasColumnType("decimal(3, 3)");

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");
            });
        }
    }
}
