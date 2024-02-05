namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedProductsModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "imageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "imageUrl");
        }
    }
}
