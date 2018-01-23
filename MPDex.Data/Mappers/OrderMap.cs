using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Data.Mappers
{
    public class OrderMap
    {
        public OrderMap(ModelBuilder builder)
        {
            builder.Entity<Order>(n =>
            {
                n.HasOne(o => o.Coin)
                 .WithMany(c => c.Orders)
                 .HasForeignKey(o => o.CoinId)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(o => o.Customer)
                 .WithMany(c => c.Orders)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(o => o.Contract)
                 .WithMany(c => c.Orders)
                 .OnDelete(DeleteBehavior.Restrict);

                n.Property(o => o.OrderCount)
                 .HasDefaultValue(0);

                n.Property(o => o.Price)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(o => o.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(o => o.Stock)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");
            });
        }
    }
}
