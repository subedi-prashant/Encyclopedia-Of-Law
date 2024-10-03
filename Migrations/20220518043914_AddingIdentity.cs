using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class AddingIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "Law",
                columns: table => new
                {
                    ID_law = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    اسمالقانون = table.Column<string>(name: "اسم القانون", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    اسممصدره = table.Column<string>(name: "اسم مصدره", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    جهةالاصدار = table.Column<string>(name: "جهة الاصدار", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    رقمالوثيقه = table.Column<string>(name: "رقم الوثيقه", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    تاريخاصدارالقانون = table.Column<string>(name: "تاريخ اصدار القانون", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    جهةالنشر = table.Column<string>(name: "جهة النشر", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    تاريخالنشر = table.Column<string>(name: "تاريخ النشر", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_القانون", x => x.ID_law);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserIssues",
                columns: table => new
                {
                    issueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    issue_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(N'pending')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserIssu__749E804CF181F235", x => x.issueID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Part[قسم(/كتاب/باب)]",
                columns: table => new
                {
                    ID_S = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_law = table.Column<int>(type: "int", nullable: true),
                    رقمالقسم = table.Column<string>(name: "رقم القسم", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    اسمالقسم = table.Column<string>(name: "اسم القسم", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_قسم(/كتاب/باب)", x => x.ID_S);
                    table.ForeignKey(
                        name: "FK_قسم(/كتاب/باب)_القانون",
                        column: x => x.ID_law,
                        principalTable: "Law",
                        principalColumn: "ID_law",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book[كتاب(/قسم/باب)]",
                columns: table => new
                {
                    ID_B = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_S = table.Column<int>(type: "int", nullable: true),
                    رقمالكتاب = table.Column<string>(name: "رقم الكتاب", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    اسمالكتاب = table.Column<string>(name: "اسم الكتاب", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_كتاب(/قسم/باب)", x => x.ID_B);
                    table.ForeignKey(
                        name: "FK_كتاب(/قسم/باب)_قسم(/كتاب/باب)",
                        column: x => x.ID_S,
                        principalTable: "Part[قسم(/كتاب/باب)]",
                        principalColumn: "ID_S",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section(كتاب/قسم)]",
                columns: table => new
                {
                    ID_P = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_B = table.Column<int>(type: "int", nullable: true),
                    رقمالباب = table.Column<string>(name: "رقم الباب", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    اسمالباب = table.Column<string>(name: "اسم الباب", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_باب(كتاب/قسم)", x => x.ID_P);
                    table.ForeignKey(
                        name: "FK_باب(كتاب/قسم)_كتاب(/قسم/باب)",
                        column: x => x.ID_B,
                        principalTable: "book[كتاب(/قسم/باب)]",
                        principalColumn: "ID_B",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapter1",
                columns: table => new
                {
                    ID_Chapter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_P = table.Column<int>(type: "int", nullable: true),
                    رقمالفصل = table.Column<string>(name: "رقم الفصل", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    اسمالفصل = table.Column<string>(name: "اسم الفصل", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_الفصل1", x => x.ID_Chapter);
                    table.ForeignKey(
                        name: "FK_الفصل1_باب(كتاب/قسم)",
                        column: x => x.ID_P,
                        principalTable: "Section(كتاب/قسم)]",
                        principalColumn: "ID_P",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapter2",
                columns: table => new
                {
                    ID_Ch2 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Chapter = table.Column<int>(type: "int", nullable: true),
                    اسمالنقطه = table.Column<string>(name: "اسم النقطه", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_فصل2", x => x.ID_Ch2);
                    table.ForeignKey(
                        name: "FK_فصل2_الفصل1",
                        column: x => x.ID_Chapter,
                        principalTable: "Chapter1",
                        principalColumn: "ID_Chapter",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapter3",
                columns: table => new
                {
                    ID_Ch3 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Ch2 = table.Column<int>(type: "int", nullable: true),
                    محتويالنقطه = table.Column<string>(name: "محتوي النقطه", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_فصل3", x => x.ID_Ch3);
                    table.ForeignKey(
                        name: "FK_فصل3_فصل2",
                        column: x => x.ID_Ch2,
                        principalTable: "Chapter2",
                        principalColumn: "ID_Ch2",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects[مواد]",
                columns: table => new
                {
                    ID_Subjects = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Chapter = table.Column<int>(type: "int", nullable: true),
                    ID_Ch2 = table.Column<int>(type: "int", nullable: true),
                    ID_Ch3 = table.Column<int>(type: "int", nullable: true),
                    رقمالماده = table.Column<string>(name: "رقم الماده", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    محتوىالماده = table.Column<string>(name: "محتوى الماده", type: "nvarchar(max)", nullable: true),
                    حالهالماده = table.Column<string>(name: "حاله الماده", type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(N'مازلت موجوده')"),
                    التعديلالسابقللماده = table.Column<string>(name: "التعديل السابق للماده", type: "nvarchar(max)", nullable: true),
                    سنهالتعديلالسابق = table.Column<string>(name: "سنه التعديل السابق", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ID_Law = table.Column<int>(type: "int", nullable: true),
                    ID_Sقسم = table.Column<int>(name: "ID_S(قسم)", type: "int", nullable: true),
                    ID_B = table.Column<int>(type: "int", nullable: true),
                    ID_P = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_مواد", x => x.ID_Subjects);
                    table.ForeignKey(
                        name: "FK_مواد_الفصل1",
                        column: x => x.ID_Chapter,
                        principalTable: "Chapter1",
                        principalColumn: "ID_Chapter",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_مواد_القانون",
                        column: x => x.ID_Law,
                        principalTable: "Law",
                        principalColumn: "ID_law",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_مواد_باب(كتاب/قسم)",
                        column: x => x.ID_P,
                        principalTable: "Section(كتاب/قسم)]",
                        principalColumn: "ID_P",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_مواد_فصل2",
                        column: x => x.ID_Ch2,
                        principalTable: "Chapter2",
                        principalColumn: "ID_Ch2",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_مواد_فصل3",
                        column: x => x.ID_Ch3,
                        principalTable: "Chapter3",
                        principalColumn: "ID_Ch3",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_مواد_قسم(/كتاب/باب)",
                        column: x => x.ID_Sقسم,
                        principalTable: "Part[قسم(/كتاب/باب)]",
                        principalColumn: "ID_S",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_مواد_كتاب(/قسم/باب)",
                        column: x => x.ID_B,
                        principalTable: "book[كتاب(/قسم/باب)]",
                        principalColumn: "ID_B",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_book[كتاب(/قسم/باب)]_ID_S",
                table: "book[كتاب(/قسم/باب)]",
                column: "ID_S");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter1_ID_P",
                table: "Chapter1",
                column: "ID_P");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter2_ID_Chapter",
                table: "Chapter2",
                column: "ID_Chapter");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter3_ID_Ch2",
                table: "Chapter3",
                column: "ID_Ch2");

            migrationBuilder.CreateIndex(
                name: "IX_Part[قسم(/كتاب/باب)]_ID_law",
                table: "Part[قسم(/كتاب/باب)]",
                column: "ID_law");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "security",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "security",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Section(كتاب/قسم)]_ID_B",
                table: "Section(كتاب/قسم)]",
                column: "ID_B");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_B",
                table: "Subjects[مواد]",
                column: "ID_B");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_Ch2",
                table: "Subjects[مواد]",
                column: "ID_Ch2");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_Ch3",
                table: "Subjects[مواد]",
                column: "ID_Ch3");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_Chapter",
                table: "Subjects[مواد]",
                column: "ID_Chapter");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_Law",
                table: "Subjects[مواد]",
                column: "ID_Law");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_P",
                table: "Subjects[مواد]",
                column: "ID_P");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects[مواد]_ID_S(قسم)",
                table: "Subjects[مواد]",
                column: "ID_S(قسم)");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "security",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "security",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "security",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "security",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "security",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Subjects[مواد]");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserIssues");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Chapter3");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Chapter2");

            migrationBuilder.DropTable(
                name: "Chapter1");

            migrationBuilder.DropTable(
                name: "Section(كتاب/قسم)]");

            migrationBuilder.DropTable(
                name: "book[كتاب(/قسم/باب)]");

            migrationBuilder.DropTable(
                name: "Part[قسم(/كتاب/باب)]");

            migrationBuilder.DropTable(
                name: "Law");
        }
    }
}
