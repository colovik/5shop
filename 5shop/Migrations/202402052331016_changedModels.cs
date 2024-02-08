namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCarts", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCarts", "status");
        }
    }
}
