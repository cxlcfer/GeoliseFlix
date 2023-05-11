using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoliseFlix.Migrations
{
    public partial class popularusuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9eb1611c-db45-4a17-844c-94f37ec2d444", "2a29ddb6-8d7d-482f-af89-b0280d04adab", "Usuário", "USUÁRIO" },
                    { "cd99dae1-a279-4a1a-9491-be038c041a41", "38645d2b-5d5d-4d9a-843c-5f220058a83b", "Moderador", "MODERADOR" },
                    { "ecd3b986-8201-4693-ae60-d2c83dd43191", "79c9725b-017f-455c-afb4-5f4100ba82da", "Administrador", "ADMINISTRADOR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1cb72cd9-ad84-491d-a711-1aeaad1bb6be", 0, "fc6655ed-eaf6-4b3b-8cba-3184c0119680", new DateTime(2005, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "AppUser", "geovanahscudeletti@gmail.com", true, false, null, "Geovana Hidalgo Scudeletti", "GEOVANAHSCUDELETTI@GMAIL.COM", "CXLCFER", "AQAAAAEAACcQAAAAEK4GFk0u+s5l5jZjPJHxnklqfqcWsK0mnxjYOl5nKDSs/rRmfeUwa2HSkgARNdSJdw==", "14991799066", true, "/img/users/avatar.png", "683c5dd6-1e2e-4d2a-801b-bc99b529900c", false, "cxlcfer" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ecd3b986-8201-4693-ae60-d2c83dd43191", "1cb72cd9-ad84-491d-a711-1aeaad1bb6be" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9eb1611c-db45-4a17-844c-94f37ec2d444");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cd99dae1-a279-4a1a-9491-be038c041a41");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ecd3b986-8201-4693-ae60-d2c83dd43191", "1cb72cd9-ad84-491d-a711-1aeaad1bb6be" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ecd3b986-8201-4693-ae60-d2c83dd43191");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1cb72cd9-ad84-491d-a711-1aeaad1bb6be");
        }
    }
}
