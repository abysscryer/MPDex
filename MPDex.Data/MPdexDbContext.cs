
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using MPDex.Data.Models;
using MPDex.Models.Base;
using System.Collections.Generic;
using MPDex.Data.Mappers;

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
        /// 고객
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// 예약
        /// </summary>
        public DbSet<Book> Book { get; set; }

        /// <summary>
        /// 코인
        /// </summary>
        public DbSet<Coin> Coin { get; set; }

        /// <summary>
        /// 잔고
        /// </summary>
        public DbSet<Balance> Balance { get; set; }


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
            new BalanceMap(modelBuilder);
        }
    }
}
