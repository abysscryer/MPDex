using System;
using MPDex.Domain;
using MPDex.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MPDex.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<MPDexContext>(options =>
                    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MPDexDBTest;Integrated Security=true;"))
                .AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            //var repository = serviceProvider.GetService<IRepository<Book>>();

            var customer = new Customer("eric", "park", "mincheol", "eric@genercrypto.com", "010-0000-0000");
            var coin = new Coin() { Id = CoinType.eth, Name = "ETH" };
            var book = new Book(BookType.Buy, 0.0005m, 0.5m, 0.5m, customer, coin);

            //var context = MPDexContext.Create();

            using (var repository = serviceProvider.GetService<IRepository<Book>>())
            {
                repository.Create(book);
                repository.SaveAsync().Wait();
            }
            Console.ReadKey();
        }
    }
}
