using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Data.Mappers
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
            
            // build forign key constrains
            builder.Entity<Coin>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Coin);

            //build fields
            builder.Entity<Coin>(n => {
                n.Property(c => c.Id)
                    .ValueGeneratedNever();
                n.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(16);
                n.Property(c => c.OnCreated)
                    .HasDefaultValueSql("getdate()")
                    .Metadata.IsReadOnlyAfterSave = true;
            });
        }
    }
}
