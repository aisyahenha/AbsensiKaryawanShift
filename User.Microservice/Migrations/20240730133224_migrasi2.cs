using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Microservice.Migrations
{
    /// <inheritdoc />
    public partial class migrasi2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_m_user_Username",
                table: "m_user",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_user_Username",
                table: "m_user");
        }
    }
}
