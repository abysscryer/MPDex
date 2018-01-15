using Microsoft.EntityFrameworkCore;
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
                n.HasKey(o => o.Id);

                n.HasOne(o => o.Statement)
                 .WithOne(s => s.Order)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(o => o.Customer)
                 .WithMany(c => c.Orders)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(o => o.Coin)
                 .WithMany(c => c.Orders)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(o => o.Contract)
                 .WithMany(c => c.Orders)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.Property(o => o.OrderType)
                 .IsRequired();
                
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
