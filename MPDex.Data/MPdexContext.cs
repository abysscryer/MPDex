
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
        /// 잔고
        /// </summary>
        public DbSet<Balance> Balance { get; set; }

        /// <summary>
        /// 예약
        /// </summary>
        public DbSet<Book> Book { get; set; }

        /// <summary>
        /// 코인
        /// </summary>
        public DbSet<Coin> Coin { get; set; }

        /// <summary>
        /// 계약
        /// </summary>
        public DbSet<Contract> Contract { get; set; }

        /// <summary>
        /// 고객
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// 수수료
        /// </summary>
        public DbSet<Fee> Fee { get; set; }

        /// <summary>
        /// 주문
        /// </summary>
        public DbSet<Order> Order { get; set; }

        /// <summary>
        /// 내역
        /// </summary>
        public DbSet<Statement> Statement { get; set; }

        /// <summary>
        /// 거래
        /// </summary>
        public DbSet<Trade> Trade { get; set; }

        /// <summary>
        /// model createing event callback
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new BalanceMap(builder);
            new BookMap(builder);
            new CoinMap(builder);
            new ContractMap(builder);
            new CustomerMap(builder);
            new FeeMap(builder);
            new OrderMap(builder);
            new StatementMap(builder);
            new TradeMap(builder);
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
