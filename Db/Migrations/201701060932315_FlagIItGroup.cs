namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlagIItGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "FlagIitGroup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "FlagIitGroup");
        }
    }
}
