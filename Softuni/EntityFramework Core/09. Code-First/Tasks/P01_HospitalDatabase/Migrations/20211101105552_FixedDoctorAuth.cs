using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_HospitalDatabase.Migrations
{
    public partial class FixedDoctorAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsAuthentication_Doctors_DoctorId",
                table: "DoctorsAuthentication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorsAuthentication",
                table: "DoctorsAuthentication");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DoctorsAuthentication");

            migrationBuilder.RenameTable(
                name: "DoctorsAuthentication",
                newName: "DoctorsAuthentications");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorsAuthentication_DoctorId",
                table: "DoctorsAuthentications",
                newName: "IX_DoctorsAuthentications_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "HashsedEmail",
                table: "DoctorsAuthentications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorsAuthentications",
                table: "DoctorsAuthentications",
                column: "HashsedEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsAuthentications_Doctors_DoctorId",
                table: "DoctorsAuthentications",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsAuthentications_Doctors_DoctorId",
                table: "DoctorsAuthentications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorsAuthentications",
                table: "DoctorsAuthentications");

            migrationBuilder.RenameTable(
                name: "DoctorsAuthentications",
                newName: "DoctorsAuthentication");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorsAuthentications_DoctorId",
                table: "DoctorsAuthentication",
                newName: "IX_DoctorsAuthentication_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "HashsedEmail",
                table: "DoctorsAuthentication",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DoctorsAuthentication",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorsAuthentication",
                table: "DoctorsAuthentication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsAuthentication_Doctors_DoctorId",
                table: "DoctorsAuthentication",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
