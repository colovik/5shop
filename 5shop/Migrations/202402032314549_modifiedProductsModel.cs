namespace _5shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedProductsModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "description", c => c.String());
            AlterColumn("dbo.Products", "name", c => c.String());
        }
    }
}
