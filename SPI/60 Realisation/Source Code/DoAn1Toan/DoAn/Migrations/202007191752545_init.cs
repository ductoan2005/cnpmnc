namespace DoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AcountID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.AcountID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        MaBL = c.Int(nullable: false, identity: true),
                        Ngay = c.DateTime(nullable: false),
                        TenBinhLuan = c.String(),
                        LaptopID = c.Int(),
                    })
                .PrimaryKey(t => t.MaBL)
                .ForeignKey("dbo.Laptops", t => t.LaptopID)
                .Index(t => t.LaptopID);
            
            CreateTable(
                "dbo.Laptops",
                c => new
                    {
                        LaptopID = c.Int(nullable: false, identity: true),
                        TenLaptop = c.String(nullable: false),
                        Giatien = c.Int(nullable: false),
                        ThongSo = c.String(nullable: false),
                        Image = c.String(),
                        MaTH = c.Int(),
                        MaTT = c.Int(),
                    })
                .PrimaryKey(t => t.LaptopID)
                .ForeignKey("dbo.ThuongHieux", t => t.MaTH)
                .ForeignKey("dbo.TinhTrangLaptops", t => t.MaTT)
                .Index(t => t.MaTH)
                .Index(t => t.MaTT);
            
            CreateTable(
                "dbo.ThuongHieux",
                c => new
                    {
                        MaTH = c.Int(nullable: false, identity: true),
                        TenThuongHieu = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaTH);
            
            CreateTable(
                "dbo.TinhTrangLaptops",
                c => new
                    {
                        MaTT = c.Int(nullable: false, identity: true),
                        TenTinhTrang = c.String(),
                    })
                .PrimaryKey(t => t.MaTT);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Laptops", "MaTT", "dbo.TinhTrangLaptops");
            DropForeignKey("dbo.Laptops", "MaTH", "dbo.ThuongHieux");
            DropForeignKey("dbo.Comments", "LaptopID", "dbo.Laptops");
            DropIndex("dbo.Laptops", new[] { "MaTT" });
            DropIndex("dbo.Laptops", new[] { "MaTH" });
            DropIndex("dbo.Comments", new[] { "LaptopID" });
            DropTable("dbo.TinhTrangLaptops");
            DropTable("dbo.ThuongHieux");
            DropTable("dbo.Laptops");
            DropTable("dbo.Comments");
            DropTable("dbo.Accounts");
        }
    }
}
