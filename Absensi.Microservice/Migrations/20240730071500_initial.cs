using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Absensi.Microservice.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_karyawan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shift = table.Column<bool>(type: "bit", nullable: false),
                    ShiftDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_karyawan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_absensi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KaryawanId = table.Column<int>(type: "int", nullable: false),
                    DateIn = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeIn = table.Column<TimeOnly>(type: "time", nullable: false),
                    StatusMasuk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOut = table.Column<TimeOnly>(type: "time", nullable: false),
                    StatusKeluar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_absensi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_absensi_m_karyawan_KaryawanId",
                        column: x => x.KaryawanId,
                        principalTable: "m_karyawan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_absensi_KaryawanId",
                table: "m_absensi",
                column: "KaryawanId");

            migrationBuilder.CreateIndex(
                name: "IX_m_karyawan_NIK",
                table: "m_karyawan",
                column: "NIK",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_absensi");

            migrationBuilder.DropTable(
                name: "m_karyawan");
        }
    }
}
