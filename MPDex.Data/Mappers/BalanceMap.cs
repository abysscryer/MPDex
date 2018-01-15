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
                n.HasKey(t => new { t.CustomerId, t.CoinId });

                n.HasOne(b => b.Customer)
                 .WithMany(c => c.Balances)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(b => b.Coin)
                 .WithMany(c => c.Balances)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasMany(b => b.Statements)
                 .WithOne(s => s.Balance)
                 .IsRequired();

                n.Property(b => b.CurrentAmount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(b => b.BookAmount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");
            });


        }
    }
}
