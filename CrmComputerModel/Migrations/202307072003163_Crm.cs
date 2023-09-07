namespace CrmComputerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        CheckId = c.Int(nullable: false, identity: true),
                        SellerID = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false),
                    })
                .PrimaryKey(t => t.CheckId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerID, cascadeDelete: true)
                .Index(t => t.SellerID)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        SellerId = c.Int(nullable: false, identity: true),
                        SellerName = c.String(),
                    })
                .PrimaryKey(t => t.SellerId);
            
            CreateTable(
                "dbo.Sells",
                c => new
                    {
                        SellId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CheckId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SellId)
                .ForeignKey("dbo.Checks", t => t.CheckId, cascadeDelete: true)
                .Index(t => t.CheckId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSells",
                c => new
                    {
                        Product_ProductId = c.Int(nullable: false),
                        Sell_SellId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductId, t.Sell_SellId })
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sells", t => t.Sell_SellId, cascadeDelete: true)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Sell_SellId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sells", "CheckId", "dbo.Checks");
            DropForeignKey("dbo.ProductSells", "Sell_SellId", "dbo.Sells");
            DropForeignKey("dbo.ProductSells", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Checks", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Checks", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ProductSells", new[] { "Sell_SellId" });
            DropIndex("dbo.ProductSells", new[] { "Product_ProductId" });
            DropIndex("dbo.Sells", new[] { "CheckId" });
            DropIndex("dbo.Checks", new[] { "CustomerId" });
            DropIndex("dbo.Checks", new[] { "SellerID" });
            DropTable("dbo.ProductSells");
            DropTable("dbo.Products");
            DropTable("dbo.Sells");
            DropTable("dbo.Sellers");
            DropTable("dbo.Customers");
            DropTable("dbo.Checks");
        }
    }
}
