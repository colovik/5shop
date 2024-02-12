namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedShoppingCartModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCarts", "OrdersHistory_id", "dbo.OrdersHistories");
            DropIndex("dbo.ShoppingCarts", new[] { "OrdersHistory_id" });
            CreateTable(
                "dbo.ShoppingCartOrdersHistories",
                c => new
                    {
                        ShoppingCart_id = c.Int(nullable: false),
                        OrdersHistory_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_id, t.OrdersHistory_id })
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_id, cascadeDelete: true)
                .ForeignKey("dbo.OrdersHistories", t => t.OrdersHistory_id, cascadeDelete: true)
                .Index(t => t.ShoppingCart_id)
                .Index(t => t.OrdersHistory_id);
            
            DropColumn("dbo.ShoppingCarts", "OrdersHistory_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCarts", "OrdersHistory_id", c => c.Int());
            DropForeignKey("dbo.ShoppingCartOrdersHistories", "OrdersHistory_id", "dbo.OrdersHistories");
            DropForeignKey("dbo.ShoppingCartOrdersHistories", "ShoppingCart_id", "dbo.ShoppingCarts");
            DropIndex("dbo.ShoppingCartOrdersHistories", new[] { "OrdersHistory_id" });
            DropIndex("dbo.ShoppingCartOrdersHistories", new[] { "ShoppingCart_id" });
            DropTable("dbo.ShoppingCartOrdersHistories");
            CreateIndex("dbo.ShoppingCarts", "OrdersHistory_id");
            AddForeignKey("dbo.ShoppingCarts", "OrdersHistory_id", "dbo.OrdersHistories", "id");
        }
    }
}
