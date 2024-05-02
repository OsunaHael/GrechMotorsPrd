using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrechMotorsPrd.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('1', 'Admin', 'ADMIN')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = '1'");
        }
    }
}
