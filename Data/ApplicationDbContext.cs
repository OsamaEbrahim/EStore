using EStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderStatus>().HasData(
                new OrderStatus
                {
                    OrderStatusId = 1,
                    Name = "InCart",
                },
                new OrderStatus
                {
                    OrderStatusId = 2,
                    Name = "Placed",
                },
                new OrderStatus
                {
                    OrderStatusId = 3,
                    Name = "Shipped",
                },
                new OrderStatus
                {
                    OrderStatusId = 4,
                    Name = "Delivered",
                },
                new OrderStatus
                {
                    OrderStatusId = 5,
                    Name = "Cancelled",
                }
            );
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductImage> ProductImage { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

    }
}
