using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGameLog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModiefiedRowVersionproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Users",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RowVersion",
                table: "Users",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);
        }
    }
}
