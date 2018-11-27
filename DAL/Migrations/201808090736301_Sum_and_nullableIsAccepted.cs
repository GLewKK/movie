namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sum_and_nullableIsAccepted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credits", "Sum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Requests", "IsAccepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "IsAccepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Credits", "Sum");
        }
    }
}
