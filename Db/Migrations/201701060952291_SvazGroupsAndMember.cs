namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SvazGroupsAndMember : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.GroupMembers", name: "Group_Uid", newName: "GroupId");
            RenameColumn(table: "dbo.GroupMembers", name: "Member_Uid", newName: "MemberId");
            RenameIndex(table: "dbo.GroupMembers", name: "IX_Group_Uid", newName: "IX_GroupId");
            RenameIndex(table: "dbo.GroupMembers", name: "IX_Member_Uid", newName: "IX_MemberId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.GroupMembers", name: "IX_MemberId", newName: "IX_Member_Uid");
            RenameIndex(table: "dbo.GroupMembers", name: "IX_GroupId", newName: "IX_Group_Uid");
            RenameColumn(table: "dbo.GroupMembers", name: "MemberId", newName: "Member_Uid");
            RenameColumn(table: "dbo.GroupMembers", name: "GroupId", newName: "Group_Uid");
        }
    }
}
