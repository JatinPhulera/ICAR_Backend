using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICAR.Scanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tree.SensorTypeId — present in entity/snapshot but missing from InitialCreate
            migrationBuilder.AddColumn<Guid>(
                name: "SensorTypeId",
                table: "Tree",
                type: "uniqueidentifier",
                nullable: true);

            // AuditTree.UpdatedBy — check if missing
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = 'AuditTree' AND COLUMN_NAME = 'UpdatedBy'
                )
                BEGIN
                    ALTER TABLE [AuditTree] ADD [UpdatedBy] nvarchar(50) NULL
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SensorTypeId", table: "Tree");
        }
    }
}
