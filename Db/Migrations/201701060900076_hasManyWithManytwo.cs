namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasManyWithManytwo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MemberMembers", newName: "MemberFriend");
            RenameColumn(table: "dbo.MemberFriend", name: "Member_Uid", newName: "MemberId");
            RenameColumn(table: "dbo.MemberFriend", name: "Member_Uid1", newName: "FriendId");
            RenameIndex(table: "dbo.MemberFriend", name: "IX_Member_Uid", newName: "IX_MemberId");
            RenameIndex(table: "dbo.MemberFriend", name: "IX_Member_Uid1", newName: "IX_FriendId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MemberFriend", name: "IX_FriendId", newName: "IX_Member_Uid1");
            RenameIndex(table: "dbo.MemberFriend", name: "IX_MemberId", newName: "IX_Member_Uid");
            RenameColumn(table: "dbo.MemberFriend", name: "FriendId", newName: "Member_Uid1");
            RenameColumn(table: "dbo.MemberFriend", name: "MemberId", newName: "Member_Uid");
            RenameTable(name: "dbo.MemberFriend", newName: "MemberMembers");
        }
    }
}
