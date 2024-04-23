using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainingEF.Migrations
{
    public partial class UpdateOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDbSet_AspNetUsers_CustomerId",
                table: "OrderDbSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_OrderId",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDbSet_CustomerId",
                table: "OrderDbSet");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderDbSet");

            migrationBuilder.RenameColumn(
                name: "Orderfulfilled",
                table: "OrderDbSet",
                newName: "OrderFulfilled");

            migrationBuilder.AddColumn<string>(
                name: "OrderId1",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderDbSet",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OrderDbSet",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_OrderId1",
                table: "OrderDetailDbSet",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDbSet_UserId",
                table: "OrderDbSet",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDbSet_AspNetUsers_UserId",
                table: "OrderDbSet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId1",
                table: "OrderDetailDbSet",
                column: "OrderId1",
                principalTable: "OrderDbSet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDbSet_AspNetUsers_UserId",
                table: "OrderDbSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_OrderId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDbSet_UserId",
                table: "OrderDbSet");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderDetailDbSet");

            migrationBuilder.RenameColumn(
                name: "OrderFulfilled",
                table: "OrderDbSet",
                newName: "Orderfulfilled");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "OrderDbSet",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderDbSet",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "OrderDbSet",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_OrderId",
                table: "OrderDetailDbSet",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDbSet_CustomerId",
                table: "OrderDbSet",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDbSet_AspNetUsers_CustomerId",
                table: "OrderDbSet",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId",
                table: "OrderDetailDbSet",
                column: "OrderId",
                principalTable: "OrderDbSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
