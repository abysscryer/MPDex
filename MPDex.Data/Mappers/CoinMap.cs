using Microsoft.EntityFrameworkCore;
using MPDex.Models;

namespace MPDex.Data.Mapper
{
    /// <summary>
    /// coin mapper
    /// </summary>
    public class CoinMap
    {
        public CoinMap(ModelBuilder builder)
        {
            // build primary key
            builder.Entity<Coin>()
                .HasKey(c => c.Id);
                
            builder.Entity<Coin>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            // build forign key constrains
            builder.Entity<Coin>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Coin);

            //build fields
            builder.Entity<Coin>(n => {
                n.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(16);
            });
        }
    }
}
