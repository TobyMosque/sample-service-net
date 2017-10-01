using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TobyMosque.Sample.Service.Net.Npgsql.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.EnsureSchema(
                name: "entity");

            migrationBuilder.CreateTable(
                name: "AuditTypes",
                schema: "domain",
                columns: table => new
                {
                    AuditTypeID = table.Column<byte>(type: "int2", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTypes", x => x.AuditTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MovimentTypes",
                schema: "domain",
                columns: table => new
                {
                    MovimentTypeID = table.Column<byte>(type: "int2", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentTypes", x => x.MovimentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "entity",
                columns: table => new
                {
                    TenantID = table.Column<int>(type: "int4", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "entity",
                columns: table => new
                {
                    ResourceID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    Observation = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Quantity = table.Column<int>(type: "int4", nullable: false),
                    TenantID = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceID);
                    table.ForeignKey(
                        name: "FK_Resources_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "entity",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    Logon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 64, nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", maxLength: 16, nullable: false),
                    TenantID = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Moviments",
                schema: "entity",
                columns: table => new
                {
                    MovimentID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    MovimentTypeID = table.Column<byte>(type: "int2", nullable: false),
                    Quantity = table.Column<int>(type: "int4", nullable: false),
                    ResourceID = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantID = table.Column<int>(type: "int4", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moviments", x => x.MovimentID);
                    table.ForeignKey(
                        name: "FK_Moviments_MovimentTypes_MovimentTypeID",
                        column: x => x.MovimentTypeID,
                        principalSchema: "domain",
                        principalTable: "MovimentTypes",
                        principalColumn: "MovimentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moviments_Resources_ResourceID",
                        column: x => x.ResourceID,
                        principalSchema: "entity",
                        principalTable: "Resources",
                        principalColumn: "ResourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moviments_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moviments_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "entity",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "entity",
                columns: table => new
                {
                    SessionID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsActive = table.Column<bool>(type: "bool", nullable: false),
                    TenantID = table.Column<int>(type: "int4", nullable: false),
                    Token = table.Column<byte[]>(type: "bytea", maxLength: 64, nullable: true),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "entity",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moviments",
                schema: "audit",
                columns: table => new
                {
                    AuditID = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AuditTypeID = table.Column<byte>(type: "int2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    MovimentID = table.Column<Guid>(type: "uuid", nullable: false),
                    MovimentTypeID = table.Column<byte>(type: "int2", nullable: false),
                    Quantity = table.Column<int>(type: "int4", nullable: false),
                    ResourceID = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionID = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantID = table.Column<int>(type: "int4", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moviments", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_Moviments_AuditTypes_AuditTypeID",
                        column: x => x.AuditTypeID,
                        principalSchema: "domain",
                        principalTable: "AuditTypes",
                        principalColumn: "AuditTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moviments_Moviments_MovimentID",
                        column: x => x.MovimentID,
                        principalSchema: "entity",
                        principalTable: "Moviments",
                        principalColumn: "MovimentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moviments_MovimentTypes_MovimentTypeID",
                        column: x => x.MovimentTypeID,
                        principalSchema: "domain",
                        principalTable: "MovimentTypes",
                        principalColumn: "MovimentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moviments_Resources_ResourceID",
                        column: x => x.ResourceID,
                        principalSchema: "entity",
                        principalTable: "Resources",
                        principalColumn: "ResourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moviments_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalSchema: "entity",
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moviments_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Moviments_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "entity",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "audit",
                columns: table => new
                {
                    AuditID = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AuditTypeID = table.Column<byte>(type: "int2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    Observation = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Quantity = table.Column<int>(type: "int4", nullable: false),
                    ResourceID = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionID = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantID = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_Resources_AuditTypes_AuditTypeID",
                        column: x => x.AuditTypeID,
                        principalSchema: "domain",
                        principalTable: "AuditTypes",
                        principalColumn: "AuditTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Resources_ResourceID",
                        column: x => x.ResourceID,
                        principalSchema: "entity",
                        principalTable: "Resources",
                        principalColumn: "ResourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalSchema: "entity",
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "audit",
                columns: table => new
                {
                    AuditID = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AuditTypeID = table.Column<byte>(type: "int2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    Logon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 64, nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", maxLength: 16, nullable: false),
                    SessionID = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantID = table.Column<int>(type: "int4", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_Users_AuditTypes_AuditTypeID",
                        column: x => x.AuditTypeID,
                        principalSchema: "domain",
                        principalTable: "AuditTypes",
                        principalColumn: "AuditTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalSchema: "entity",
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalSchema: "entity",
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "entity",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_AuditTypeID",
                schema: "audit",
                table: "Moviments",
                column: "AuditTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_IsDeleted",
                schema: "audit",
                table: "Moviments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_MovimentID",
                schema: "audit",
                table: "Moviments",
                column: "MovimentID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_MovimentTypeID",
                schema: "audit",
                table: "Moviments",
                column: "MovimentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_ResourceID",
                schema: "audit",
                table: "Moviments",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_SessionID",
                schema: "audit",
                table: "Moviments",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_UserID",
                schema: "audit",
                table: "Moviments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_TenantID_AuditDate",
                schema: "audit",
                table: "Moviments",
                columns: new[] { "TenantID", "AuditDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_AuditTypeID",
                schema: "audit",
                table: "Resources",
                column: "AuditTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_IsDeleted",
                schema: "audit",
                table: "Resources",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceID",
                schema: "audit",
                table: "Resources",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_SessionID",
                schema: "audit",
                table: "Resources",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TenantID_AuditDate",
                schema: "audit",
                table: "Resources",
                columns: new[] { "TenantID", "AuditDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuditTypeID",
                schema: "audit",
                table: "Users",
                column: "AuditTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                schema: "audit",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SessionID",
                schema: "audit",
                table: "Users",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserID",
                schema: "audit",
                table: "Users",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantID_AuditDate",
                schema: "audit",
                table: "Users",
                columns: new[] { "TenantID", "AuditDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_IsDeleted",
                schema: "entity",
                table: "Moviments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_MovimentTypeID",
                schema: "entity",
                table: "Moviments",
                column: "MovimentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_ResourceID",
                schema: "entity",
                table: "Moviments",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_UserID",
                schema: "entity",
                table: "Moviments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Moviments_TenantID_CreationDate",
                schema: "entity",
                table: "Moviments",
                columns: new[] { "TenantID", "CreationDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_IsDeleted",
                schema: "entity",
                table: "Resources",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TenantID_CreationDate",
                schema: "entity",
                table: "Resources",
                columns: new[] { "TenantID", "CreationDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TenantID_Description",
                schema: "entity",
                table: "Resources",
                columns: new[] { "TenantID", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Token",
                schema: "entity",
                table: "Sessions",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserID",
                schema: "entity",
                table: "Sessions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TenantID_CreationDate",
                schema: "entity",
                table: "Sessions",
                columns: new[] { "TenantID", "CreationDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Description",
                schema: "entity",
                table: "Tenants",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                schema: "entity",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantID_CreationDate",
                schema: "entity",
                table: "Users",
                columns: new[] { "TenantID", "CreationDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantID_Logon",
                schema: "entity",
                table: "Users",
                columns: new[] { "TenantID", "Logon" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moviments",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "Moviments",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "AuditTypes",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "MovimentTypes",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "entity");
        }
    }
}
