using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainingEF.Migrations
{
    public partial class UpdateProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_ProductId",
                table: "OrderDetailDbSet");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ProductDbSet",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "OrderDetailDbSet",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_ProductId1",
                table: "OrderDetailDbSet",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId1",
                table: "OrderDetailDbSet",
                column: "ProductId1",
                principalTable: "ProductDbSet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailDbSet_ProductId1",
                table: "OrderDetailDbSet");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderDetailDbSet");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductDbSet",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailDbSet_ProductId",
                table: "OrderDetailDbSet",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailDbSet_ProductDbSet_ProductId",
                table: "OrderDetailDbSet",
                column: "ProductId",
                principalTable: "ProductDbSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
