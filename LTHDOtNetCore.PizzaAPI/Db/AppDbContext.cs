using LTHDOtNetCore.PizzaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.PizzaAPI
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<PizzaExtraModel> PizzaExtras { get; set; }
        public DbSet<PizzaOrderModel> PizzaOrders { get; set; }
        public DbSet<PizzaOrderDetailModel> PizzaOrderDetails { get; set; }

    }
}
