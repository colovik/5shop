namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedOrderHistoryModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ShoppingCartProducts", newName: "ProductShoppingCarts");
            DropPrimaryKey("dbo.ProductShoppingCarts");
            CreateTable(
                "dbo.OrdersHistories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.ShoppingCarts", "OrdersHistory_id", c => c.Int());
            AddPrimaryKey("dbo.ProductShoppingCarts", new[] { "Product_id", "ShoppingCart_id" });
            CreateIndex("dbo.ShoppingCarts", "OrdersHistory_id");
            AddForeignKey("dbo.ShoppingCarts", "OrdersHistory_id", "dbo.OrdersHistories", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "OrdersHistory_id", "dbo.OrdersHistories");
            DropIndex("dbo.ShoppingCarts", new[] { "OrdersHistory_id" });
            DropPrimaryKey("dbo.ProductShoppingCarts");
            DropColumn("dbo.ShoppingCarts", "OrdersHistory_id");
            DropTable("dbo.OrdersHistories");
            AddPrimaryKey("dbo.ProductShoppingCarts", new[] { "ShoppingCart_id", "Product_id" });
            RenameTable(name: "dbo.ProductShoppingCarts", newName: "ShoppingCartProducts");
        }
    }
}
