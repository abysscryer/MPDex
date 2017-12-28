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

        public static MPDexContext Create() {

            //var section = ConfigurationManager.GetSection("");
            var contextOptions = new DbContextOptionsBuilder<MPDexContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MPDexDBTest;Integrated Security=true;")
                .Options;

            return new MPDexContext(contextOptions);
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }

        public Func<DateTime> TimestampProvider { get; set; } = ()
            => DateTime.UtcNow;

        public Customer UserProvider
        {
            get
            {
                if (true)
                    return new Customer();
                return new Customer();
            }
        }

        public override int SaveChanges()
        {
            TrackChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IAuditable)
                {
                    var auditable = entry.Entity as IAuditable;
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedBy = UserProvider;  
                        auditable.CreatedOn = TimestampProvider();
                        auditable.UpdatedOn = TimestampProvider();
                    }
                    else
                    {
                        auditable.UpdatedBy = UserProvider;
                        auditable.UpdatedOn = TimestampProvider();
                    }
                }
            }
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CustomerMap(modelBuilder);
            new BookMap(modelBuilder);
        }
    }
}
