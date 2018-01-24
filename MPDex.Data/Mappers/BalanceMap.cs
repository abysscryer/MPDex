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
            builder.Entity<Balance>(n =>
            {
                n.HasKey("CustomerId", "CoinId");

                n.HasOne(b => b.Customer)
                 .WithMany(c => c.Balances)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(b => b.Coin)
                 .WithMany(c => c.Balances)
                 .OnDelete(DeleteBehavior.Restrict);

                n.Property(b => b.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)")
                 .HasDefaultValue(0);
            });
        }
    }
}
