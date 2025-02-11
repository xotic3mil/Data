using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addedGUIDForProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add new temporary column
            migrationBuilder.AddColumn<Guid>(
                name: "NewProjectNumber",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()");

            // Step 2: Update new column with UUIDs
            migrationBuilder.Sql(
                @"UPDATE ""Projects"" SET ""NewProjectNumber"" = uuid_generate_v4();");

            // Step 3: Drop the old ProjectNumber column
            migrationBuilder.DropColumn(
                name: "ProjectNumber",
                table: "Projects");

            // Step 4: Rename NewProjectNumber to ProjectNumber
            migrationBuilder.RenameColumn(
                name: "NewProjectNumber",
                table: "Projects",
                newName: "ProjectNumber");

            // Step 5: Recreate index if it existed on ProjectNumber
            // Uncomment and modify the following line if you had an index
            // migrationBuilder.CreateIndex(
            //     name: "IX_Projects_ProjectNumber",
            //     table: "Projects",
            //     column: "ProjectNumber",
            //     unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the operations for Down migration

            // Step 1: Add old ProjectNumber column as int
            migrationBuilder.AddColumn<int>(
                name: "OldProjectNumber",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Step 2: Optionally, convert UUIDs back to integers (not practical)
            // For simplicity, set default values or handle accordingly

            // Step 3: Drop the ProjectNumber column (uuid)
            migrationBuilder.DropColumn(
                name: "ProjectNumber",
                table: "Projects");

            // Step 4: Rename OldProjectNumber back to ProjectNumber
            migrationBuilder.RenameColumn(
                name: "OldProjectNumber",
                table: "Projects",
                newName: "ProjectNumber");

            // Step 5: Recreate index if it existed on ProjectNumber
            // Uncomment and modify the following line if you had an index
            // migrationBuilder.CreateIndex(
            //     name: "IX_Projects_ProjectNumber",
            //     table: "Projects",
            //     column: "ProjectNumber",
            //     unique: true);
        }
    }
}
