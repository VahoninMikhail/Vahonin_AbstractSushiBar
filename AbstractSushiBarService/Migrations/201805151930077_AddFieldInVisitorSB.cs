namespace AbstractSushiBarService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldInVisitorSB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visitors", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visitors", "Mail");
        }
    }
}
