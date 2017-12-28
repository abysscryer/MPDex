using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPDex.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Repository.DomainMap
{
    public class BookMap
    {
        public BookMap(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasKey(t => t.Id);
            builder.Entity<Book>()
                .HasOne(b => b.Coin)
                .WithMany(c => c.Books);
            builder.Entity<Book>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Books);

            //builder.Entity<Book>().HasBaseType<Auditable<Guid>>();
            builder.Entity<Book>(n => {
                n.Property(b => b.Id)
                    .ValueGeneratedOnAdd();
                n.Property(f => f.Price)
                    .IsRequired()
                    .HasColumnType("decimal(20, 8)");
                n.Property(f => f.Amount)
                    .IsRequired()
                    .HasColumnType("decimal(20, 8)");
                n.Property(f => f.Stock)
                    .IsRequired()
                    .HasColumnType("decimal(20, 8)");
                n.Property(f => f.CreatedOn)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                n.Property(f => f.UpdatedOn)
                    .ValueGeneratedOnUpdate();
            });
        }
    }
}
