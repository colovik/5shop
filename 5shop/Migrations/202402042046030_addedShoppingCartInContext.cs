namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedShoppingCartInContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false),
                        total = c.Int(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Products", "ShoppingCart_id", c => c.Int());
            CreateIndex("dbo.Products", "ShoppingCart_id");
            AddForeignKey("dbo.Products", "ShoppingCart_id", "dbo.ShoppingCarts", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ShoppingCart_id", "dbo.ShoppingCarts");
            DropIndex("dbo.Products", new[] { "ShoppingCart_id" });
            DropColumn("dbo.Products", "ShoppingCart_id");
            DropTable("dbo.ShoppingCarts");
        }
    }
}
