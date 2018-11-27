namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CreditorId = c.String(),
                        ModeratorId = c.String(),
                        StatusId = c.Guid(nullable: false),
                        CreditId = c.Guid(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credits", t => t.CreditId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.CreditId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Telephones",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CountryName = c.String(),
                        NumberCode = c.String(),
                        NumberExample = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DataExtras", "Telephone", c => c.String());
            AddColumn("dbo.DataExtras", "FirstName", c => c.String());
            AddColumn("dbo.DataExtras", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Requests", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.Credits", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "CreditId" });
            DropIndex("dbo.Requests", new[] { "StatusId" });
            DropIndex("dbo.Requests", new[] { "UserId" });
            DropIndex("dbo.Credits", new[] { "UserId" });
            DropColumn("dbo.DataExtras", "LastName");
            DropColumn("dbo.DataExtras", "FirstName");
            DropColumn("dbo.DataExtras", "Telephone");
            DropTable("dbo.Telephones");
            DropTable("dbo.Status");
            DropTable("dbo.Requests");
            DropTable("dbo.Credits");
        }
    }
}
