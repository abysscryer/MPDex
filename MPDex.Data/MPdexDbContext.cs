
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MPDex.Data.Mapper;
using MPDex.Models;
using MPDex.Data.Models;
using MPDex.Models.Base;
using System.Collections.Generic;

namespace MPDex.Data
{

    public class MPDexDbContext : IdentityDbContext<MPDexUser, MPDexRole, string>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public MPDexDbContext(DbContextOptions<MPDexDbContext> options) 
            : base(options)
        { }
        
        /// <summary>
        /// customer model
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// book model
        /// </summary>
        public DbSet<Book> Book { get; set; }

        /// <summary>
        /// coin model
        /// </summary>
        public DbSet<Coin> Coin { get; set; }

        /// <summary>
        /// model createing event callback
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CustomerMap(modelBuilder);
            new BookMap(modelBuilder);
            new CoinMap(modelBuilder);
        }
    }
}
