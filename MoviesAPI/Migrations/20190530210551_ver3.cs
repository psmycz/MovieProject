using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesAPI.Migrations.MovieAPIDb
{
    public partial class ver3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UsersRating",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsersRating",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
