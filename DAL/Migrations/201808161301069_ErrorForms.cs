namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorForms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormErrors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestId = c.Guid(nullable: false),
                        FormId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormErrors", "RequestId", "dbo.Requests");
            DropIndex("dbo.FormErrors", new[] { "RequestId" });
            DropTable("dbo.FormErrors");
        }
    }
}
