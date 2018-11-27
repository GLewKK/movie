namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataExtra_instead_UserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Credits", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Credits", new[] { "UserId" });
            AddColumn("dbo.Credits", "DataExtraId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Credits", "DataExtraId");
            AddForeignKey("dbo.Credits", "DataExtraId", "dbo.DataExtras", "Id", cascadeDelete: true);
            DropColumn("dbo.Credits", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Credits", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Credits", "DataExtraId", "dbo.DataExtras");
            DropIndex("dbo.Credits", new[] { "DataExtraId" });
            DropColumn("dbo.Credits", "DataExtraId");
            CreateIndex("dbo.Credits", "UserId");
            AddForeignKey("dbo.Credits", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
