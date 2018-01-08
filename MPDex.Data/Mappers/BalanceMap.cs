using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Data.Mappers
{
    public class BalanceMap
    {
        public BalanceMap(ModelBuilder builder)
        {
            builder.Entity<Balance>()
                .HasKey(t => new { t.CustomerId, t.CoinId });

            builder.Entity<Balance>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Balances)
                .HasForeignKey(b => b.CustomerId);

            builder.Entity<Balance>()
                .HasOne(b => b.Coin)
                .WithMany(c => c.Balances)
                .HasForeignKey(b => b.CoinId);

            builder.Entity<Balance>(n =>
            {
                n.Property(b => b.Amount).IsRequired().HasColumnType("decimal(20, 8)");
            });


        }
    }
}
