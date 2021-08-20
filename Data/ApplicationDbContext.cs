using EStore.Models;
using Microsoft.AspNetCore.Identity;
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
            base.OnModelCreating(builder);
            SeedUsers(builder);
            SeedRoles(builder);
            SeedUserRoles(builder);
            SeedOrderStatus(builder);
            SeedCategories(builder);
            SeedProducts(builder);
            SeedProductsImages(builder);
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductImage> ProductImage { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "97493f9a-d6f0-4347-ac2b-2e34eacce787",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin_123");

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "70f24812-654f-4f11-850c-eea6b6c92e43", Name = "Admin", ConcurrencyStamp = "44961e9b-7815-4734-93e5-a413a6250026", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "6c924bb6-517f-4cf3-b47e-fdb539f51ed7", Name = "Customer", ConcurrencyStamp = "26415719-a297-4c49-bb75-801689659960", NormalizedName = "CUSTOMER" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "70f24812-654f-4f11-850c-eea6b6c92e43", UserId = "97493f9a-d6f0-4347-ac2b-2e34eacce787" }
                );
        }
        private void SeedOrderStatus(ModelBuilder builder)
        {
            builder.Entity<OrderStatus>().HasData(
                new OrderStatus { OrderStatusId = 1, Name = "InCart"},
                new OrderStatus { OrderStatusId = 2, Name = "Placed"},
                new OrderStatus { OrderStatusId = 3, Name = "Shipped" },
                new OrderStatus { OrderStatusId = 4, Name = "Delivered" },
                new OrderStatus { OrderStatusId = 5, Name = "Cancelled" }
                );
        }
        private void SeedCategories(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "GPU", ThumbnailPath = "Images/Categories/GPU/70df7603-4531-4a95-8cc5-8ffaf317a5ba_GPU.jpg" },
                new Category { CategoryId = 2, Name = "CPU", ThumbnailPath = "Images/Categories/CPU/36c9229c-7791-4d7c-ab88-2a81938fc605_CPU.jpg" },
                new Category { CategoryId = 3, Name = "PSU", ThumbnailPath = "Images/Categories/PSU/9b8d32fc-c2c5-4877-9ca0-257de8ab9911_PowerSupply.jpg" },
                new Category { CategoryId = 4, Name = "Motherboard", ThumbnailPath = "Images/Categories/Motherboard/74ae4d8b-f885-437f-814e-9b80655348b6_Motherboard.jpg" }
                );
        }
        private void SeedProducts(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "2080 TI", Description = "High End GPU", Cost = 555, Price = 800, Stock = 10 , CategoryId = 1  },
                new Product { ProductId = 2, Name = "1050 TI", Description = "Budget GPU", Cost = 40, Price = 55, Stock = 100, CategoryId = 1 },
                new Product { ProductId = 3, Name = "1080 TI", Description = "Mid Tier GPU", Cost = 200, Price = 270, Stock = 35, CategoryId = 1 },
                new Product { ProductId = 4, Name = "3090", Description = "High End GPU", Cost = 600, Price = 850, Stock = 5, CategoryId = 1 },
                new Product { ProductId = 5, Name = "MSI GeForce RTX 3070", Description = "High End GPU", Cost = 450, Price = 600, Stock = 20, CategoryId = 1 },
                new Product { ProductId = 6, Name = "Intel 6600", Description = "Budget CPU", Cost = 60, Price = 70, Stock = 40, CategoryId = 2 },
                new Product { ProductId = 7, Name = "AMD Ryzen 5 5600X", Description = "Mid Tier CPU", Cost = 200, Price = 270, Stock = 35, CategoryId = 2 },
                new Product { ProductId = 8, Name = "EVGA SuperNOVA 750", Description = "Fully Modular", Cost = 50, Price = 65, Stock = 40, CategoryId = 3 },
                new Product { ProductId = 9, Name = "ROG Strix Z590-E", Description = "Gaming Motherboard", Cost = 220, Price = 350, Stock = 20, CategoryId = 4 },
                new Product { ProductId = 10, Name = "ASUS ROG Strix B550-F", Description = "Gaming Motherboard", Cost = 200, Price = 300, Stock = 15, CategoryId = 4 }


                );
        }
        private void SeedProductsImages(ModelBuilder builder)
        {
            builder.Entity<ProductImage>().HasData(
                new ProductImage { ProductImageId = 1, ProductId = 1, Path = "Images/Products/2080 TI/6149b4b1-23e1-4091-b90d-ea6f898fa379_8188NNMGDOL._AC_SL1500_.jpg" },
                new ProductImage { ProductImageId = 2, ProductId = 1, Path = "Images/Products/2080 TI/abf1d17d-4d9d-4880-9c1a-a6f5fb6bee58_10819_1Z.jpg" },
                new ProductImage { ProductImageId = 3, ProductId = 1, Path = "Images/Products/2080 TI/7f4115f4-33f3-4637-b0c3-80d63ecb306f_7216808_1582585820.jpg" },
                new ProductImage { ProductImageId = 4, ProductId = 2, Path = "Images/Products/1050 TI/63512bbe-87ba-491b-a449-7584151d7338_s-l640.jpg" },
                new ProductImage { ProductImageId = 5, ProductId = 2, Path = "Images/Products/1050 TI/1af130ca-b94f-4af4-9478-1cb1e6aa6029_download.jpg" },
                new ProductImage { ProductImageId = 6, ProductId = 3, Path = "Images/Products/1080TI/53759a31-6644-4c18-92d6-554024bda16f_81IdRzJZHeL._AC_SY450_.jpg" },
                new ProductImage { ProductImageId = 7, ProductId = 4, Path = "Images/Products/3090/fbe133bc-0a9b-4206-aea2-8929af6db12f_nvidia-rtx-3090-gpu.jpg" },
                new ProductImage { ProductImageId = 8, ProductId = 5, Path = "Images/Products/MSI GeForce RTX 3070/c4fc8a9a-cf89-4985-9a37-9bb451ac3854_MSI_GeForce_RTX_3070_Gaming_X_Trio_4.jpg" },
                new ProductImage { ProductImageId = 9, ProductId = 6, Path = "Images/Products/Intel 6600/0991c40e-07c0-43dd-92b4-679b7571bcc2_19-117-562-02.jpg" },
                new ProductImage { ProductImageId = 10, ProductId = 7, Path = "Images/Products/AMD Ryzen 5 5600X/89e11c53-3509-4a50-87fe-fa5092d7f5c8_amd-ryzen-5-5600x-3.7ghz-cpu.jpg" },
                new ProductImage { ProductImageId = 11, ProductId = 8, Path = "Images/Products/EVGA SuperNOVA 750/83a43357-9ca8-40de-99f9-5ef9fa371a5e_ccc71417a0f95e13d49d8dd33ffabd1d-hi.jpg" },
                new ProductImage { ProductImageId = 12, ProductId = 9, Path = "Images/Products/ROG Strix Z590-E/b8c6e825-a816-4270-9bb6-f1836ce75142_asus-rog-strix-z590-e-gaming-wifi-motherboard-3.jpg" },
                new ProductImage { ProductImageId = 13, ProductId = 10, Path = "Images/Products/ASUS ROG Strix B550-F/7f2843fb-5c44-4124-afc4-d1c871e79b42_81x069mwcbL._AC_SL1500_.jpg" }
                );
        }

    }

}

