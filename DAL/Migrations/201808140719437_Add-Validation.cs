namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DataExtras", "Telephone", c => c.String(maxLength: 50));
            AlterColumn("dbo.DataExtras", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.DataExtras", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.DataExtras", "IDNP", c => c.String(maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DataExtras", "IDNP", c => c.String());
            AlterColumn("dbo.DataExtras", "LastName", c => c.String());
            AlterColumn("dbo.DataExtras", "FirstName", c => c.String());
            AlterColumn("dbo.DataExtras", "Telephone", c => c.String());
        }
    }
}
