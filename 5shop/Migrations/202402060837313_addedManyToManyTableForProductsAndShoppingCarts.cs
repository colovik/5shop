namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedManyToManyTableForProductsAndShoppingCarts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ShoppingCart_id", "dbo.ShoppingCarts");
            DropIndex("dbo.Products", new[] { "ShoppingCart_id" });
            CreateTable(
                "dbo.ShoppingCartProducts",
                c => new
                    {
                        ShoppingCart_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_id, t.Product_id })
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .Index(t => t.ShoppingCart_id)
                .Index(t => t.Product_id);
            
            DropColumn("dbo.Products", "ShoppingCart_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ShoppingCart_id", c => c.Int());
            DropForeignKey("dbo.ShoppingCartProducts", "Product_id", "dbo.Products");
            DropForeignKey("dbo.ShoppingCartProducts", "ShoppingCart_id", "dbo.ShoppingCarts");
            DropIndex("dbo.ShoppingCartProducts", new[] { "Product_id" });
            DropIndex("dbo.ShoppingCartProducts", new[] { "ShoppingCart_id" });
            DropTable("dbo.ShoppingCartProducts");
            CreateIndex("dbo.Products", "ShoppingCart_id");
            AddForeignKey("dbo.Products", "ShoppingCart_id", "dbo.ShoppingCarts", "id");
        }
    }
}
