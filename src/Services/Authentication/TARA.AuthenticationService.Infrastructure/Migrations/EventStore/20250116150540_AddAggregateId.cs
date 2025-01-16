using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TARA.AuthenticationService.Infrastructure.Migrations.EventStore
{
    /// <inheritdoc />
    public partial class AddAggregateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AggregateId",
                schema: "Authentication",
                table: "Events",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregateId",
                schema: "Authentication",
                table: "Events");
        }
    }
}
