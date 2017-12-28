using Microsoft.EntityFrameworkCore;
using MPDex.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Repository.DomainMap
{
    public class CustomerMap
    {
        public CustomerMap(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(t => t.Id);
            //builder.Entity<Customer>().HasBaseType<Recordable<Guid>>();
            builder.Entity<Customer>(t =>
            {
                t.Property(f => f.NiceName)
                    .IsRequired()
                    .HasMaxLength(16);
                t.Property(f => f.FamilyName)
                    .IsRequired()
                    .HasMaxLength(16);
                t.Property(f => f.GivenName)
                    .IsRequired()
                    .HasMaxLength(16);
                t.Property(f => f.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(36);
                t.Property(f => f.CellPhone)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(20);
                t.Property(f => f.CreatedOn)
                    .IsRequired();
            }); 
        }
    }
}
