using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainingEF.Migrations
{
    public partial class UpdateOrderDetailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_OrderId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_ProductId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderDetailDbSet");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_OrderId",
                table: "OrderDetailDbSet",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_ProductId",
                table: "OrderDetailDbSet",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId",
                table: "OrderDetailDbSet",
                column: "OrderId",
                principalTable: "OrderDbSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId",
                table: "OrderDetailDbSet",
                column: "ProductId",
                principalTable: "ProductDbSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId",
                table: "OrderDetailDbSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_OrderId",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_ProductId",
                table: "OrderDetailDbSet");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderDetailDbSet",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderDetailDbSet",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderDetailDbSet",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "OrderId1",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_OrderId1",
                table: "OrderDetailDbSet",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_ProductId1",
                table: "OrderDetailDbSet",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_OrderDbSet_OrderId1",
                table: "OrderDetailDbSet",
                column: "OrderId1",
                principalTable: "OrderDbSet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId1",
                table: "OrderDetailDbSet",
                column: "ProductId1",
                principalTable: "ProductDbSet",
                principalColumn: "Id");
        }
    }
}
