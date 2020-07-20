namespace DoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "BinhLuan", c => c.String(nullable: false));
            AddColumn("dbo.Comments", "AcountID", c => c.Int());
            CreateIndex("dbo.Comments", "AcountID");
            AddForeignKey("dbo.Comments", "AcountID", "dbo.Accounts", "AcountID");
            DropColumn("dbo.Comments", "TenBinhLuan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "TenBinhLuan", c => c.String());
            DropForeignKey("dbo.Comments", "AcountID", "dbo.Accounts");
            DropIndex("dbo.Comments", new[] { "AcountID" });
            DropColumn("dbo.Comments", "AcountID");
            DropColumn("dbo.Comments", "BinhLuan");
        }
    }
}
