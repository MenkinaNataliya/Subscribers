namespace Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Uid = c.Long(nullable: false),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        Deactivated = c.String(),
                        Photo = c.String(),
                        Member_Uid = c.Long(),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Members", t => t.Member_Uid)
                .Index(t => t.Member_Uid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "Member_Uid", "dbo.Members");
            DropIndex("dbo.Members", new[] { "Member_Uid" });
            DropTable("dbo.Members");
        }
    }
}
