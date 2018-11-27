namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullabeDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DataExtras", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DataExtras", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
