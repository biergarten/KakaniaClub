using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mas.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignToUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInitiated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Person_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Person_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Person_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Person_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReferralProcessInfo_ReferralStatus = table.Column<int>(type: "int", nullable: true),
                    ReferralProcessInfo_HasEmailBeenMoved = table.Column<bool>(type: "bit", nullable: true),
                    ReferralProcessInfo_EmailOriginalLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralProcessInfo_EmailNewLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
