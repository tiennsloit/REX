namespace REX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isDelete_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsDeleted");
        }
    }
}
