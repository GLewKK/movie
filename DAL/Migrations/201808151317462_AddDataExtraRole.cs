namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataExtraRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataExtras", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataExtras", "Role");
        }
    }
}
