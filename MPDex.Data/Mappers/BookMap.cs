using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using MPDex.Models.Domain;

namespace MPDex.Data.Mappers
{
    /// <summary>
    /// book mapper
    /// </summary>
    public class BookMap
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="builder"></param>
        public BookMap(ModelBuilder builder)
        {
            builder.Entity<Book>(n => {

                n.HasOne(b => b.Coin)
                 .WithMany(c => c.Books)
                 .HasForeignKey(b => b.CoinId)
                 .OnDelete(DeleteBehavior.Restrict);

                n.HasOne(b => b.Customer)
                 .WithMany(c => c.Books)
                 .OnDelete(DeleteBehavior.Restrict);
                
                n.Property(b => b.BookStatus)
                 .HasDefaultValue(BookStatus.Placed);

                n.Property(b => b.OrderCount)
                 .HasDefaultValue(0);

                n.Property(b => b.Price)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(b => b.Amount)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(b => b.Stock)
                 .IsRequired()
                 .HasColumnType("decimal(20, 8)");

                n.Property(b => b.OnCreated)
                 .HasDefaultValueSql("getdate()");

                n.Property(b => b.OnUpdated)
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
