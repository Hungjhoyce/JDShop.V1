using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JDShop.Migrations
{
    /// <inheritdoc />
    public partial class JDShopDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount_percent = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UseNumber = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products_Inventorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    QuantitySold = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CDT = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avartar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Point = table.Column<int>(type: "int", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    AccountTypeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountType",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_Role",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CDT = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Suppliers",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Account",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CreateDay = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oders_Account",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Oders_Discount",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodsId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Oders",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods",
                        column: x => x.PaymentMethodsId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CollectionId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CDT = table.Column<DateTime>(type: "datetime2", fixedLength: true, maxLength: 10, nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionProduct_Collection",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Items",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductSize_ColorId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Items", x => new { x.OrderId, x.ProductSize_ColorId });
                    table.ForeignKey(
                        name: "FK_Oder_Items_Oders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSize_Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    Product_InventoryId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSize_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSize_Colors_Colors",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSize_Colors_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSize_Colors_Products_Inventorys",
                        column: x => x.Product_InventoryId,
                        principalTable: "Products_Inventorys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSize_Colors_Size",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CDT = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTypes_Product",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receipt_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSize_ColorId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDay = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_Products_ProductSize_Colors",
                        column: x => x.ProductSize_ColorId,
                        principalTable: "ProductSize_Colors",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AccountType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bạc" },
                    { 2, "Vàng" },
                    { 3, "Kim Cương" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Color" },
                values: new object[,]
                {
                    { 1, "Đen" },
                    { 2, "Trắng" },
                    { 3, "Xanh" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Thanh toán bằng tiền mặt khi nhận hàng", "Tiền mặt" },
                    { 2, "Thanh toán bằng chuyển khoản ngân hàng", "Chuyển khoản" }
                });

            migrationBuilder.InsertData(
                table: "Products_Inventorys",
                columns: new[] { "Id", "Quantity", "QuantitySold" },
                values: new object[,]
                {
                    { 1, 100, 0 },
                    { 2, 150, 0 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Nhân Viên" },
                    { 3, "Khách Hàng" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { 1, "S" },
                    { 2, "M" },
                    { 3, "L" },
                    { 4, "XL" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "CDT", "Email", "Name", "Phone", "Status" },
                values: new object[,]
                {
                    { 1, "123 Ha Noi", new DateTime(2025, 5, 28, 0, 4, 12, 722, DateTimeKind.Local).AddTicks(6379), "supplier@jdshop.com", "JDShop Supplier", "0123456789", true },
                    { 2, "456 Quang Ninh", new DateTime(2025, 5, 28, 0, 4, 12, 722, DateTimeKind.Local).AddTicks(6396), "fashion@supplier.com", "Fashion Supplier", "0987654321", true }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountTypeId", "Avartar", "Birthday", "Email", "FullName", "Gender", "Password", "Phone", "Point", "RoleId", "Status", "UserName" },
                values: new object[,]
                {
                    { 1, 3, null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@jdshop.com", "Admin JDShop", (byte)1, "4297f44b13955235245b2497399d7a93", "0123456789", null, 1, (byte)1, "jdshop.admin" },
                    { 2, 1, null, new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "khachang1@gmail.com", "Khách Hàng 1", (byte)2, "4297f44b13955235245b2497399d7a93", "0987654321", 100, 2, (byte)1, "customer1" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CDT", "Description", "Name", "Status", "SupplierId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 28, 0, 4, 12, 722, DateTimeKind.Local).AddTicks(6427), "Các loại áo dành cho nữ", "Áo nữ", true, 1 },
                    { 2, new DateTime(2025, 5, 28, 0, 4, 12, 722, DateTimeKind.Local).AddTicks(6430), "Các loại áo dành cho nam", "Áo nam", true, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "CategoryId", "CDT", "Description", "Name", "ProductId", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 28, 0, 4, 12, 722, DateTimeKind.Local).AddTicks(6455), "Áo thun dành cho nữ", "Áo thun nữ", null, (byte)1 },
                    { 2, 2, new DateTime(2025, 5, 28, 0, 4, 12, 722, DateTimeKind.Local).AddTicks(6459), "Áo sơ mi dành cho nam", "Áo sơ mi nam", null, (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "ProductTypeId", "Status" },
                values: new object[,]
                {
                    { 1, null, "Áo thun nữ form rộng basic", "Áo thun nữ basic", 199000.0, 1, (byte)1 },
                    { 2, null, "Áo sơ mi nam dài tay", "Áo sơ mi nam công sở", 299000.0, 2, (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "ProductSize_Colors",
                columns: new[] { "Id", "Code", "ColorId", "ProductId", "Product_InventoryId", "SizeId" },
                values: new object[,]
                {
                    { 1, "PSC001", 1, 1, 1, 1 },
                    { 2, "PSC002", 2, 1, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AccountId",
                table: "Address",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SupplierId",
                table: "Category",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProduct_CollectionId",
                table: "CollectionProduct",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProduct_ProductId",
                table: "CollectionProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_ProductSize_ColorId",
                table: "Order_Items",
                column: "ProductSize_ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountId",
                table: "Orders",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrdersId",
                table: "Payments",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodsId",
                table: "Payments",
                column: "PaymentMethodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_Colors_ColorId",
                table: "ProductSize_Colors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_Colors_Product_InventoryId",
                table: "ProductSize_Colors",
                column: "Product_InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_Colors_ProductId",
                table: "ProductSize_Colors",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_Colors_SizeId",
                table: "ProductSize_Colors",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_CategoryId",
                table: "ProductTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductId",
                table: "ProductTypes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_Products_ProductSize_ColorId",
                table: "Receipt_Products",
                column: "ProductSize_ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionProduct_Products",
                table: "CollectionProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oder_Items_ProductSize_Colors",
                table: "Order_Items",
                column: "ProductSize_ColorId",
                principalTable: "ProductSize_Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Suppliers",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Product",
                table: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CollectionProduct");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Order_Items");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Receipt_Products");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ProductSize_Colors");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Products_Inventorys");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
