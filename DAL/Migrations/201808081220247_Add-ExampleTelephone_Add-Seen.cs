namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExampleTelephone_AddSeen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Seen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Seen");
        }
    }
}
