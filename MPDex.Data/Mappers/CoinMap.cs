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
            builder.Entity<Coin>(n => {
                
                n.HasKey(c => c.Id);
                
                n.HasMany(c => c.Books)
                 .WithOne(b => b.Coin)
                 .IsRequired();

                n.HasMany(c => c.Trades)
                 .WithOne(t => t.Coin)
                 .IsRequired();

                n.HasMany(c => c.Orders)
                 .WithOne(o => o.Coin)
                 .IsRequired();

                n.HasMany(c => c.Balances)
                 .WithOne(b => b.Coin)
                 .IsRequired();

                n.HasMany(c => c.Statements)
                 .WithOne(s => s.Coin)
                 .IsRequired();

                n.Property(c => c.Id)
                    .ValueGeneratedNever();

                n.Property(c => c.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(16);

                n.Property(c => c.OnCreated)
                    .HasDefaultValueSql("getdate()");
            });
        }
    }
}
