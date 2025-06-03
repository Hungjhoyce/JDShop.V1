using Microsoft.EntityFrameworkCore;
using JDshop.Models;

namespace JDshop.Data;

public static class DbFake
{
    public static void FakeData(ModelBuilder modelBuilder)
    {
        // Seed Roles
        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Nhân Viên" },
            new Role { Id = 3, Name = "Khách Hàng" }
        };
        modelBuilder.Entity<Role>().HasData(roles);

        // Seed AccountTypes
        var accountTypes = new List<AccountType>
        {
            new AccountType { Id = 1, Name = "Bạc" },
            new AccountType { Id = 2, Name = "Vàng" },
            new AccountType { Id = 3, Name = "Kim Cương" }
        };
        modelBuilder.Entity<AccountType>().HasData(accountTypes);

        // Seed Accounts
        var accounts = new List<Account>
        {
            new Account
            {
                Id = 1,
                UserName = "jdshop.admin",
                Password = "4297f44b13955235245b2497399d7a93", // 123123
                Email = "admin@jdshop.com",
                FullName = "Admin JDShop",
                Phone = "0123456789",
                Birthday = new DateTime(1990, 1, 1),
                Gender = 1,
                RoleId = 1,
                AccountTypeId = 3,
                Status = 1
            },
            new Account
            {
                Id = 2,
                UserName = "customer1",
                Password = "4297f44b13955235245b2497399d7a93", // 123123
                Email = "khachang1@gmail.com",
                FullName = "Khách Hàng 1",
                Phone = "0987654321",
                Point = 100,
                Birthday = new DateTime(1995, 5, 15),
                Gender = 2,
                RoleId = 2,
                AccountTypeId = 1,
                Status = 1
            }
        };
        modelBuilder.Entity<Account>().HasData(accounts);

        // Seed Suppliers
        var suppliers = new List<Supplier>
        {
            new Supplier
            {
                Id = 1,
                Name = "JDShop Supplier",
                Address = "123 Ha Noi",
                Phone = "0123456789",
                Email = "supplier@jdshop.com",
                Status = true,
                Cdt = DateTime.Now
            },
            new Supplier
            {
                Id = 2,
                Name = "Fashion Supplier",
                Address = "456 Quang Ninh",
                Phone = "0987654321",
                Email = "fashion@supplier.com",
                Status = true,
                Cdt = DateTime.Now
            }
        };
        modelBuilder.Entity<Supplier>().HasData(suppliers);

        // Seed Categories
        var categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Áo nữ",
                SupplierId = 1,
                Description = "Các loại áo dành cho nữ",
                Status = true,
                Cdt = DateTime.Now
            },
            new Category
            {
                Id = 2,
                Name = "Áo nam",
                SupplierId = 1,
                Description = "Các loại áo dành cho nam",
                Status = true,
                Cdt = DateTime.Now
            }
        };
        modelBuilder.Entity<Category>().HasData(categories);

        // Seed ProductTypes
        var productTypes = new List<ProductType>
        {
            new ProductType
            {
                Id = 1,
                CategoryId = 1,
                Name = "Áo thun nữ",
                Description = "Áo thun dành cho nữ",
                Status = 1,
                Cdt = DateTime.Now
            },
            new ProductType
            {
                Id = 2,
                CategoryId = 2,
                Name = "Áo sơ mi nam",
                Description = "Áo sơ mi dành cho nam",
                Status = 1,
                Cdt = DateTime.Now
            }
        };
        modelBuilder.Entity<ProductType>().HasData(productTypes);

        // Seed Products
        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Áo thun nữ basic",
                Description = "Áo thun nữ form rộng basic",
                Price = 199000,
                Status = 1,
                ProductTypeId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Áo sơ mi nam công sở",
                Description = "Áo sơ mi nam dài tay",
                Price = 299000,
                Status = 1,
                ProductTypeId = 2
            }
        };
        modelBuilder.Entity<Product>().HasData(products);

        // Seed Colors
        var colors = new List<Color>
        {
            new Color { Id = 1, Color1 = "Đen" },
            new Color { Id = 2, Color1 = "Trắng" },
            new Color { Id = 3, Color1 = "Xanh" }
        };
        modelBuilder.Entity<Color>().HasData(colors);

        // Seed Sizes
        var sizes = new List<Size>
        {
            new Size { Id = 1, Size1 = "S" },
            new Size { Id = 2, Size1 = "M" },
            new Size { Id = 3, Size1 = "L" },
            new Size { Id = 4, Size1 = "XL" }
        };
        modelBuilder.Entity<Size>().HasData(sizes);

        // Seed Product Inventory
        var inventories = new List<ProductsInventory>
        {
            new ProductsInventory { Id = 1, Quantity = 100, QuantitySold = 0 },
            new ProductsInventory { Id = 2, Quantity = 150, QuantitySold = 0 }
        };
        modelBuilder.Entity<ProductsInventory>().HasData(inventories);

        // Seed ProductSizeColors
        var productSizeColors = new List<ProductSizeColor>
        {
            new ProductSizeColor
            {
                Id = 1,
                ProductId = 1,
                SizeId = 1,
                ColorId = 1,
                ProductInventoryId = 1,
                Code = "PSC001"
            },
            new ProductSizeColor
            {
                Id = 2,
                ProductId = 1,
                SizeId = 2,
                ColorId = 2,
                ProductInventoryId = 2,
                Code = "PSC002"
            }
        };
        modelBuilder.Entity<ProductSizeColor>().HasData(productSizeColors);

        // Seed PaymentMethods
        var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Tiền mặt", Description = "Thanh toán bằng tiền mặt khi nhận hàng" },
            new PaymentMethod { Id = 2, Name = "Chuyển khoản", Description = "Thanh toán bằng chuyển khoản ngân hàng" }
        };
        modelBuilder.Entity<PaymentMethod>().HasData(paymentMethods);
    }
} 