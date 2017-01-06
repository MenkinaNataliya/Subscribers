namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SvazGroupsAndMember2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupMembers", "MemberId", "dbo.Members");
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropIndex("dbo.GroupMembers", new[] { "MemberId" });
            DropTable("dbo.GroupMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupId = c.String(nullable: false, maxLength: 128),
                        MemberId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.MemberId });
            
            CreateIndex("dbo.GroupMembers", "MemberId");
            CreateIndex("dbo.GroupMembers", "GroupId");
            AddForeignKey("dbo.GroupMembers", "MemberId", "dbo.Members", "Uid", cascadeDelete: true);
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Uid", cascadeDelete: true);
        }
    }
}
