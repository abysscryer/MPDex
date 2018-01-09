using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Data.Mappers
{
    /// <summary>
    /// customer mapper
    /// </summary>
    public class CustomerMap
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="builder"></param>
        public CustomerMap(ModelBuilder builder)
        {
            // build constraints
            builder.Entity<Customer>()
                .HasKey(c => c.Id);

            // build fields
            builder.Entity<Customer>(n =>
            {
                n.Property(b => b.Id)
                    .HasDefaultValueSql("newid()");
                n.Property(c => c.NiceName)
                    .IsRequired()
                    .HasMaxLength(16);
                n.Property(c => c.FamilyName)
                    .IsRequired()
                    .HasMaxLength(16);
                n.Property(c => c.GivenName)
                    .IsRequired()
                    .HasMaxLength(16);
                n.Property(c => c.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(36);
                n.Property(c => c.CellPhone)
                    .IsUnicode(false)
                    .HasMaxLength(20);
                n.Property(c => c.OnCreated)
                    .IsRequired()
                    .HasDefaultValueSql("getdate()");
            }); 
        }
    }
}
