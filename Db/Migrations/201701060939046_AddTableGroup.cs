namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Uid = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Uid);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        Group_Uid = c.String(nullable: false, maxLength: 128),
                        Member_Uid = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Uid, t.Member_Uid })
                .ForeignKey("dbo.Groups", t => t.Group_Uid, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Uid, cascadeDelete: true)
                .Index(t => t.Group_Uid)
                .Index(t => t.Member_Uid);
            
            DropColumn("dbo.Members", "FlagIitGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "FlagIitGroup", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.GroupMembers", "Member_Uid", "dbo.Members");
            DropForeignKey("dbo.GroupMembers", "Group_Uid", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "Member_Uid" });
            DropIndex("dbo.GroupMembers", new[] { "Group_Uid" });
            DropTable("dbo.GroupMembers");
            DropTable("dbo.Groups");
        }
    }
}
