using Microsoft.EntityFrameworkCore;
using MPDex.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Repository.DomainMap
{
    /// <summary>
    /// coin mapper
    /// </summary>
    public class CoinMap
    {
        public CoinMap(ModelBuilder builder)
        {
            // build constraints
            builder.Entity<Coin>().HasKey(c => c.Id);
            builder.Entity<Coin>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Coin);

            //build fields
            builder.Entity<Coin>(n => {
                n.Property(c => c.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(16);

            });
        }
    }
}
