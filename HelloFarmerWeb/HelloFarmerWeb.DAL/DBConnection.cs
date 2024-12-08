using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloFarmerWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HelloFarmerWeb.DAL
{
    public class DBConnection : DbContext
    {
        public DbSet<Account> Account { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HelloFarmer;" +
                                    "Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                                    "Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
