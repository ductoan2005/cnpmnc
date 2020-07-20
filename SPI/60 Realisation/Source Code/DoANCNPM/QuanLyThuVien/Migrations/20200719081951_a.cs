using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyThuVien.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiDocGia",
                columns: table => new
                {
                    LoaiDocGiaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDocGia = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiDocGia", x => x.LoaiDocGiaID);
                });

            migrationBuilder.CreateTable(
                name: "TheLoaiSach",
                columns: table => new
                {
                    TheLoaiSachID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTLSach = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiSach", x => x.TheLoaiSachID);
                });

            migrationBuilder.CreateTable(
                name: "TinhTrangPhieuMuon",
                columns: table => new
                {
                    TinhTrangPhieuMuonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinhTrangPM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhTrangPhieuMuon", x => x.TinhTrangPhieuMuonID);
                });

            migrationBuilder.CreateTable(
                name: "TinhTrangSach",
                columns: table => new
                {
                    TinhTrangSachID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTTSach = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhTrangSach", x => x.TinhTrangSachID);
                });

            migrationBuilder.CreateTable(
                name: "DocGia",
                columns: table => new
                {
                    DocGiaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDocGia = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    DiaChi = table.Column<string>(nullable: false),
                    NgayLapThe = table.Column<DateTime>(nullable: false),
                    SLSachDaMuon = table.Column<int>(nullable: false),
                    NgayHetHan = table.Column<DateTime>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false),
                    ConLai = table.Column<double>(nullable: false),
                    LoaiDocGiaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocGia", x => x.DocGiaID);
                    table.ForeignKey(
                        name: "FK_DocGia_LoaiDocGia_LoaiDocGiaID",
                        column: x => x.LoaiDocGiaID,
                        principalTable: "LoaiDocGia",
                        principalColumn: "LoaiDocGiaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    SachID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSach = table.Column<string>(nullable: false),
                    HinhAnh = table.Column<string>(nullable: true),
                    NhaXB = table.Column<string>(nullable: false),
                    NgayNhap = table.Column<DateTime>(nullable: false),
                    NamHetHan = table.Column<DateTime>(nullable: false),
                    TinhTrangSachID = table.Column<int>(nullable: true),
                    Gia = table.Column<int>(nullable: false),
                    TacGia = table.Column<string>(nullable: false),
                    NgayXB = table.Column<DateTime>(nullable: false),
                    TheLoaiSachID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.SachID);
                    table.ForeignKey(
                        name: "FK_Sach_TheLoaiSach_TheLoaiSachID",
                        column: x => x.TheLoaiSachID,
                        principalTable: "TheLoaiSach",
                        principalColumn: "TheLoaiSachID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sach_TinhTrangSach_TinhTrangSachID",
                        column: x => x.TinhTrangSachID,
                        principalTable: "TinhTrangSach",
                        principalColumn: "TinhTrangSachID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuMuon",
                columns: table => new
                {
                    PhieuMuonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayMuon = table.Column<DateTime>(nullable: false),
                    TinhTrangPhieuMuonID = table.Column<int>(nullable: true),
                    TienCoc = table.Column<double>(nullable: false),
                    DocGiaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuMuon", x => x.PhieuMuonID);
                    table.ForeignKey(
                        name: "FK_PhieuMuon_DocGia_DocGiaID",
                        column: x => x.DocGiaID,
                        principalTable: "DocGia",
                        principalColumn: "DocGiaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuMuon_TinhTrangPhieuMuon_TinhTrangPhieuMuonID",
                        column: x => x.TinhTrangPhieuMuonID,
                        principalTable: "TinhTrangPhieuMuon",
                        principalColumn: "TinhTrangPhieuMuonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuTra",
                columns: table => new
                {
                    PhieuTraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayTra = table.Column<DateTime>(nullable: false),
                    TienPhat = table.Column<int>(nullable: false),
                    DocGiaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuTra", x => x.PhieuTraID);
                    table.ForeignKey(
                        name: "FK_PhieuTra_DocGia_DocGiaID",
                        column: x => x.DocGiaID,
                        principalTable: "DocGia",
                        principalColumn: "DocGiaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(nullable: false),
                    SachID = table.Column<int>(nullable: true),
                    DocGiaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Cart_DocGia_DocGiaID",
                        column: x => x.DocGiaID,
                        principalTable: "DocGia",
                        principalColumn: "DocGiaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Sach_SachID",
                        column: x => x.SachID,
                        principalTable: "Sach",
                        principalColumn: "SachID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CT_PhieuTra",
                columns: table => new
                {
                    CT_PhieuTraID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoNgayMuon = table.Column<int>(nullable: false),
                    SachID = table.Column<int>(nullable: true),
                    PhieuTraID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PhieuTra", x => x.CT_PhieuTraID);
                    table.ForeignKey(
                        name: "FK_CT_PhieuTra_PhieuTra_PhieuTraID",
                        column: x => x.PhieuTraID,
                        principalTable: "PhieuTra",
                        principalColumn: "PhieuTraID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CT_PhieuTra_Sach_SachID",
                        column: x => x.SachID,
                        principalTable: "Sach",
                        principalColumn: "SachID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuThuTienPhat",
                columns: table => new
                {
                    PhieuThuTienPhatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TienCoc = table.Column<double>(nullable: false),
                    TienPhat = table.Column<double>(nullable: false),
                    TienConLai = table.Column<double>(nullable: false),
                    DocGiaID = table.Column<int>(nullable: true),
                    PhieuTraID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuThuTienPhat", x => x.PhieuThuTienPhatID);
                    table.ForeignKey(
                        name: "FK_PhieuThuTienPhat_DocGia_DocGiaID",
                        column: x => x.DocGiaID,
                        principalTable: "DocGia",
                        principalColumn: "DocGiaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuThuTienPhat_PhieuTra_PhieuTraID",
                        column: x => x.PhieuTraID,
                        principalTable: "PhieuTra",
                        principalColumn: "PhieuTraID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CT_PhieuMuon",
                columns: table => new
                {
                    CT_PhieuMuonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SachID = table.Column<int>(nullable: true),
                    CartID = table.Column<int>(nullable: true),
                    PhieuMuonID = table.Column<int>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PhieuMuon", x => x.CT_PhieuMuonID);
                    table.ForeignKey(
                        name: "FK_CT_PhieuMuon_Cart_CartID",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CT_PhieuMuon_PhieuMuon_PhieuMuonID",
                        column: x => x.PhieuMuonID,
                        principalTable: "PhieuMuon",
                        principalColumn: "PhieuMuonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CT_PhieuMuon_Sach_SachID",
                        column: x => x.SachID,
                        principalTable: "Sach",
                        principalColumn: "SachID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TinhTrangPhieuMuon",
                columns: new[] { "TinhTrangPhieuMuonID", "TinhTrangPM" },
                values: new object[,]
                {
                    { 1, "Chưa Trả" },
                    { 2, "Đã Trả" }
                });

            migrationBuilder.InsertData(
                table: "TinhTrangSach",
                columns: new[] { "TinhTrangSachID", "TenTTSach" },
                values: new object[,]
                {
                    { 1, "Trống" },
                    { 2, "Cho Thuê" },
                    { 3, "Mất" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_DocGiaID",
                table: "Cart",
                column: "DocGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_SachID",
                table: "Cart",
                column: "SachID");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuMuon_CartID",
                table: "CT_PhieuMuon",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuMuon_PhieuMuonID",
                table: "CT_PhieuMuon",
                column: "PhieuMuonID");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuMuon_SachID",
                table: "CT_PhieuMuon",
                column: "SachID");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuTra_PhieuTraID",
                table: "CT_PhieuTra",
                column: "PhieuTraID");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhieuTra_SachID",
                table: "CT_PhieuTra",
                column: "SachID");

            migrationBuilder.CreateIndex(
                name: "IX_DocGia_LoaiDocGiaID",
                table: "DocGia",
                column: "LoaiDocGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuon_DocGiaID",
                table: "PhieuMuon",
                column: "DocGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuon_TinhTrangPhieuMuonID",
                table: "PhieuMuon",
                column: "TinhTrangPhieuMuonID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuThuTienPhat_DocGiaID",
                table: "PhieuThuTienPhat",
                column: "DocGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuThuTienPhat_PhieuTraID",
                table: "PhieuThuTienPhat",
                column: "PhieuTraID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuTra_DocGiaID",
                table: "PhieuTra",
                column: "DocGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_TheLoaiSachID",
                table: "Sach",
                column: "TheLoaiSachID");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_TinhTrangSachID",
                table: "Sach",
                column: "TinhTrangSachID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_PhieuMuon");

            migrationBuilder.DropTable(
                name: "CT_PhieuTra");

            migrationBuilder.DropTable(
                name: "PhieuThuTienPhat");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "PhieuMuon");

            migrationBuilder.DropTable(
                name: "PhieuTra");

            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "TinhTrangPhieuMuon");

            migrationBuilder.DropTable(
                name: "DocGia");

            migrationBuilder.DropTable(
                name: "TheLoaiSach");

            migrationBuilder.DropTable(
                name: "TinhTrangSach");

            migrationBuilder.DropTable(
                name: "LoaiDocGia");
        }
    }
}
