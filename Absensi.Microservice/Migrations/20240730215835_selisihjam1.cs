using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Absensi.Microservice.Migrations
{
    /// <inheritdoc />
    public partial class selisihjam1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelisihJamMasukKeluar",
                table: "m_absensi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelisihJamMasukKeluar",
                table: "m_absensi");
        }
    }
}
