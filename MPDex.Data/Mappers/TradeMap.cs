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
                n.HasOne(t => t.Coin)
                 .WithMany(c => c.Trades)
                 .HasForeignKey(t => t.CoinId)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(t => t.Customer)
                 .WithMany(c => c.Trades)
                 .OnDelete(DeleteBehavior.Restrict);
                
                n.Property(b => b.Price)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(b => b.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");
            });
        }
    }
}
