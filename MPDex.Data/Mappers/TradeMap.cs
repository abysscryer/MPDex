using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Data.Mappers
{
    public class TradeMap
    {
        public TradeMap(ModelBuilder builder)
        {
            builder.Entity<Trade>(n =>
            {
                n.HasKey(t => t.Id);

                n.HasOne(t => t.Coin)
                 .WithMany(c => c.Trades)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasMany(t => t.Contracts)
                 .WithOne(c => c.Trade)
                 .IsRequired();

                n.Property(b => b.Id)
                    .HasDefaultValueSql("newid()");

                n.Property(b => b.Price)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(b => b.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");
            });
        }
    }
}
