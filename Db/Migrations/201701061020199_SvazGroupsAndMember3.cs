namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SvazGroupsAndMember3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupId = c.String(nullable: false, maxLength: 128),
                        MemberId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.MemberId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupMembers", "MemberId", "dbo.Members");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "MemberId" });
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropTable("dbo.GroupMembers");
        }
    }
}
