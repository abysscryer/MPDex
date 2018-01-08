using Microsoft.EntityFrameworkCore;
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
            // build constraints
            builder.Entity<Book>()
                .HasKey(t => t.Id);

            // build forign key constratins with coin
            builder.Entity<Book>()
                .HasOne(b => b.Coin)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CoinId);

            // build forign key constratins with customer
            builder.Entity<Book>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CustomerId);

            // build fields
            builder.Entity<Book>(n => {
                //n.Property(b => b.Id)
                    //.HasDefaultValueSql("newid()");
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
                    .IsRequired()
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
