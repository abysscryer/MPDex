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
