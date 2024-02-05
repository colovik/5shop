namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Cart_id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Cart_id");
            AddForeignKey("dbo.AspNetUsers", "Cart_id", "dbo.ShoppingCarts", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Cart_id", "dbo.ShoppingCarts");
            DropIndex("dbo.AspNetUsers", new[] { "Cart_id" });
            DropColumn("dbo.AspNetUsers", "Cart_id");
        }
    }
}
