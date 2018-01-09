
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MPDex.Data.Mappers;
using MPDex.Models.Domain;

namespace MPDex.Data
{

    public class MPDexContext : IdentityDbContext<Operator, OperatorRole, string>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public MPDexContext(DbContextOptions<MPDexContext> options) 
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

    /// <summary>
    /// 관리자
    /// </summary>
    public class Operator : IdentityUser
    { }

    /// <summary>
    /// 관리자 역할
    /// </summary>
    public class OperatorRole : IdentityRole
    { }
}
