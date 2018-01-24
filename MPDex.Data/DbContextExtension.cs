using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;
using System.Collections.Generic;
using MPDex.Models.Domain;
using GenFu;
using System;

namespace MPDex.Data
{
    public static class DbContextExtension
    {

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this MPDexContext context)
        {
           
            List<Coin> coins = new List<Coin>();

            if (!context.Fee.Any())
            {
                var coinNames = new string[] { "Bitcoin", "Ethereum", "Ripple" };
                short i = 0;

                A.Configure<Coin>()
                    .Fill(c => c.Id, () => { return ++i; })
                    .Fill(c => c.Name, () => { return coinNames[i]; });
                    
                coins = GenFu.GenFu.ListOf<Coin>(coinNames.Length);

                short j = 0;

                A.Configure<Fee>()
                    .Fill(f => f.Id, () => { return ++j; })
                    .Fill(f => f.Coin, () => { return coins.ElementAt(j); })
                    .Fill(f => f.Percent).WithRandom(new decimal[] { 2.0m, 2.5m });

                var fees = A.ListOf<Fee>(coinNames.Length);
                
                context.AddRange(fees);
                context.SaveChanges();
            }

            //Ensure we have some status
            if (!context.Customer.Any())
            {
                var i = 0;

                A.Configure<Balance>()
                     .Fill(b => b.CoinId, () => { return (short)((i++ % coins.Count) + 1); })
                     .Fill(b => b.Amount).WithinRange(10, 99)
                     .Fill(b => b.CustomerId, Guid.Empty);

                //var balances = A.ListOf<Balance>(coins.Count);

                A.Configure<Customer>()
                    .Fill(c => c.Email).AsEmailAddressForDomain("mpdex.com")
                    .Fill(c => c.NickName).AsFirstName()
                    .Fill(c => c.FamilyName).AsLastName()
                    .Fill(c => c.GivenName).AsFirstName()
                    .Fill(c => c.CellPhone).AsPhoneNumber()
                    .Fill(c => c.Balances, () => { return A.ListOf<Balance>(coins.Count);  });
                    
                var customers = A.ListOf<Customer>(10);
                //customers.ForEach(x => { x.Balances = balances; });
                context.AddRange(customers);
                context.SaveChanges();

            }
        }
    }
}
