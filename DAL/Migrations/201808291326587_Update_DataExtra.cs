namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_DataExtra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credits", "Months", c => c.Int(nullable: false));
            AddColumn("dbo.DataExtras", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.DataExtras", "WorkSpace", c => c.String(maxLength: 50));
            AddColumn("dbo.DataExtras", "TelephoneWorkSpace", c => c.String(maxLength: 20));
            AddColumn("dbo.DataExtras", "OfficialVenit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DataExtras", "IsMarried", c => c.Boolean(nullable: false));
            AddColumn("dbo.DataExtras", "WorkSpaceHusbandWife", c => c.String(maxLength: 50));
            AddColumn("dbo.DataExtras", "OfficialVenitHusbandWife", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DataExtras", "IDNP", c => c.String(maxLength: 20));
            AlterColumn("dbo.DataExtras", "Role", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DataExtras", "Role", c => c.String());
            AlterColumn("dbo.DataExtras", "IDNP", c => c.String(maxLength: 13));
            DropColumn("dbo.DataExtras", "OfficialVenitHusbandWife");
            DropColumn("dbo.DataExtras", "WorkSpaceHusbandWife");
            DropColumn("dbo.DataExtras", "IsMarried");
            DropColumn("dbo.DataExtras", "OfficialVenit");
            DropColumn("dbo.DataExtras", "TelephoneWorkSpace");
            DropColumn("dbo.DataExtras", "WorkSpace");
            DropColumn("dbo.DataExtras", "DateOfBirth");
            DropColumn("dbo.Credits", "Months");
        }
    }
}
