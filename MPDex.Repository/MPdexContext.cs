using Microsoft.EntityFrameworkCore;
using MPDex.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using MPDex.Repository.DomainMap;

namespace MPDex.Repository
{
    public class MPDexContext : DbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public MPDexContext(DbContextOptions<MPDexContext> options) : base(options)
        { }
        
        /// <summary>
        /// customer model
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// book model
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// model createing event callback
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CustomerMap(modelBuilder);
            new BookMap(modelBuilder);
        }
    }
}
