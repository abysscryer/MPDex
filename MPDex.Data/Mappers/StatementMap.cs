﻿using Microsoft.EntityFrameworkCore;
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
                n.HasKey(s => s.Id);

                n.HasOne(s => s.Order)
                 .WithOne(o => o.Statement)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Customer)
                 .WithMany(c => c.Statements)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Coin)
                 .WithMany(c => c.Statements)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Balance)
                 .WithMany(b => b.Statements)
                 .HasForeignKey("CustomerId", "CoinId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(s => s.Fee)
                 .WithMany(f => f.Statements);

                n.Property(s => s.BeforeAmount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(s => s.AfterAmount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(s => s.BalanceAmount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(s => s.FeeAmount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(s => s.VerifyKey)
                 .IsRequired()
                 .IsUnicode(false)
                 .HasMaxLength(64);

                n.Property(f => f.OnCreated)
                 .HasDefaultValueSql("getdate()");

                n.Property(f => f.OnUpdated)
                 .ValueGeneratedOnUpdate();

                n.Property(b => b.IPAddress)
                 .IsUnicode(false)
                 .HasMaxLength(36)
                 .IsRequired();

                n.Property(b => b.RowVersion)
                 .IsRequired()
                 .IsRowVersion();

            });
        }
    }
}
