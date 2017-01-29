namespace REX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        FaceBookName = c.String(maxLength: 1000),
                        Phone1 = c.String(maxLength: 100),
                        Phone2 = c.String(maxLength: 100),
                        Address = c.String(maxLength: 1000),
                        DistrictId = c.Int(nullable: false),
                        TimeCanReceivedId = c.Int(nullable: false),
                        HowManyDaysOfConsume = c.Int(nullable: false),
                        HowManyWeightOfConsume = c.Int(nullable: false),
                        NextShipDate = c.DateTime(nullable: false),
                        Satisfied = c.String(maxLength: 50),
                        Unsatisfied = c.String(maxLength: 50),
                        ReasonNotSatisfied = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.TimeADays", t => t.TimeCanReceivedId, cascadeDelete: true)
                .Index(t => t.DistrictId)
                .Index(t => t.TimeCanReceivedId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        RiceTypeId = c.Int(nullable: false),
                        Price1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCurrently = c.Boolean(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.RiceTypes", t => t.RiceTypeId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.RiceTypeId);
            
            CreateTable(
                "dbo.RiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeADays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeInDay = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RiceType1Id = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Surcharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountToReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoverPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PromoPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShipFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Received = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateShipped = c.DateTime(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.RiceTypes", t => t.RiceType1Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.RiceType1Id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        IsActived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "RiceType1Id", "dbo.RiceTypes");
            DropForeignKey("dbo.Orders", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "TimeCanReceivedId", "dbo.TimeADays");
            DropForeignKey("dbo.Favourites", "RiceTypeId", "dbo.RiceTypes");
            DropForeignKey("dbo.Favourites", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "DistrictId", "dbo.Districts");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "RiceType1Id" });
            DropIndex("dbo.Orders", new[] { "ContactId" });
            DropIndex("dbo.Favourites", new[] { "RiceTypeId" });
            DropIndex("dbo.Favourites", new[] { "ContactId" });
            DropIndex("dbo.Contacts", new[] { "TimeCanReceivedId" });
            DropIndex("dbo.Contacts", new[] { "DistrictId" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.TimeADays");
            DropTable("dbo.RiceTypes");
            DropTable("dbo.Favourites");
            DropTable("dbo.Districts");
            DropTable("dbo.Contacts");
        }
    }
}
