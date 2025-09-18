using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ATMData.Data;

namespace ATMData
{
    public class ATMContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public string DbPath { get; }

        public ATMContext(DbContextOptions<ATMContext> options) : base(options) 
        {

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "atm.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
