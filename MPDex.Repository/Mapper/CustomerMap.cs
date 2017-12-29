using Microsoft.EntityFrameworkCore;
using MPDex.Models;

namespace MPDex.Data.Mapper
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
            builder.Entity<Customer>().HasKey(c => c.Id);
            
            // build fields
            builder.Entity<Customer>(n =>
            {
                n.Property(c => c.Id)
                    .ValueGeneratedOnAdd();
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
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(20);
                n.Property(c => c.CreatedOn)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                n.Property(c => c.UpdatedOn)
                    .ValueGeneratedOnUpdate();
            }); 
        }
    }
}
