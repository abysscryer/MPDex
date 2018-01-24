using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Data.Mappers
{
    public class StatementMap
    {
        public StatementMap(ModelBuilder builder)
        {
            builder.Entity<Statement>(n => {
                
                n.HasOne(s => s.Customer)
                 .WithMany(c => c.Statements)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Coin)
                 .WithMany(c => c.Statements)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Balance)
                 .WithMany(b => b.Statements)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Fee)
                 .WithMany(f => f.Statements)
                 .OnDelete(DeleteBehavior.Restrict);
                
                n.Property(s => s.BeforeAmount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(s => s.AfterAmount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(s => s.BalanceAmount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(s => s.FeeAmount)
                 .IsRequired()
                 .HasColumnType("decimal(28, 8)");

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");
                
            });
        }
    }
}
