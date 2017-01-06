namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasManyWithMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "Member_Uid", "dbo.Members");
            DropIndex("dbo.Members", new[] { "Member_Uid" });
            CreateTable(
                "dbo.MemberMembers",
                c => new
                    {
                        Member_Uid = c.Long(nullable: false),
                        Member_Uid1 = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_Uid, t.Member_Uid1 })
                .ForeignKey("dbo.Members", t => t.Member_Uid)
                .ForeignKey("dbo.Members", t => t.Member_Uid1)
                .Index(t => t.Member_Uid)
                .Index(t => t.Member_Uid1);
            
            DropColumn("dbo.Members", "Member_Uid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Member_Uid", c => c.Long());
            DropForeignKey("dbo.MemberMembers", "Member_Uid1", "dbo.Members");
            DropForeignKey("dbo.MemberMembers", "Member_Uid", "dbo.Members");
            DropIndex("dbo.MemberMembers", new[] { "Member_Uid1" });
            DropIndex("dbo.MemberMembers", new[] { "Member_Uid" });
            DropTable("dbo.MemberMembers");
            CreateIndex("dbo.Members", "Member_Uid");
            AddForeignKey("dbo.Members", "Member_Uid", "dbo.Members", "Uid");
        }
    }
}
