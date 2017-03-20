using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CkeckIn.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "discipline_entity",
                columns: table => new
                {
                    DisciplineId = table.Column<Guid>(nullable: false),
                    DisciplineName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discipline_entity", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "application_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "identity_usertoken<_guid>",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_usertoken<_guid>", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "identity_userclaim<_guid>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_userclaim<_guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_userclaim<_guid>_application_user_UserId",
                        column: x => x.UserId,
                        principalTable: "application_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_userlogin<_guid>",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_userlogin<_guid>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_identity_userlogin<_guid>_application_user_UserId",
                        column: x => x.UserId,
                        principalTable: "application_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thread_entity",
                columns: table => new
                {
                    ThreadId = table.Column<Guid>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    ThreadName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thread_entity", x => x.ThreadId);
                    table.ForeignKey(
                        name: "FK_thread_entity_discipline_entity_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "discipline_entity",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_roleclaim<_guid>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_roleclaim<_guid>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_roleclaim<_guid>_application_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "application_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_userrole<_guid>",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_userrole<_guid>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_identity_userrole<_guid>_application_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "application_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_identity_userrole<_guid>_application_user_UserId",
                        column: x => x.UserId,
                        principalTable: "application_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question_entity",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    QuestionMessage = table.Column<string>(maxLength: 1024, nullable: true),
                    RepositoryId = table.Column<Guid>(nullable: false),
                    Strong = table.Column<short>(nullable: false),
                    ThreadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_entity", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_question_entity_thread_entity_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "thread_entity",
                        principalColumn: "ThreadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test_entity",
                columns: table => new
                {
                    TestId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    FactoryMethod = table.Column<int>(nullable: false),
                    QuestionCountLimit = table.Column<int>(nullable: false),
                    ThreadId = table.Column<Guid>(nullable: false),
                    TimetoPerformInSeconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_entity", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_test_entity_application_user_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "application_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_entity_thread_entity_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "thread_entity",
                        principalColumn: "ThreadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answer_entity",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(nullable: false),
                    AnswerMessage = table.Column<string>(maxLength: 256, nullable: true),
                    Mark = table.Column<short>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer_entity", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_answer_entity_question_entity_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "question_entity",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_testentity",
                columns: table => new
                {
                    StudentTestId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: true),
                    CompleteDateTime = table.Column<DateTime>(nullable: false),
                    NumberAnswers = table.Column<int>(nullable: false),
                    StartedDateTime = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false),
                    ValidAnswers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_testentity", x => x.StudentTestId);
                    table.ForeignKey(
                        name: "FK_student_testentity_application_user_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "application_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_student_testentity_application_user_StudentId",
                        column: x => x.StudentId,
                        principalTable: "application_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_testentity_test_entity_TestId",
                        column: x => x.TestId,
                        principalTable: "test_entity",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test_questionentity",
                columns: table => new
                {
                    TestQuestionId = table.Column<Guid>(nullable: false),
                    Position = table.Column<short>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_questionentity", x => x.TestQuestionId);
                    table.ForeignKey(
                        name: "FK_test_questionentity_question_entity_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "question_entity",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_questionentity_test_entity_TestId",
                        column: x => x.TestId,
                        principalTable: "test_entity",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_answerentity",
                columns: table => new
                {
                    StudentTestAnswerId = table.Column<Guid>(nullable: false),
                    AnswedTime = table.Column<DateTime>(nullable: false),
                    AnswerId = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_answerentity", x => x.StudentTestAnswerId);
                    table.ForeignKey(
                        name: "FK_student_answerentity_answer_entity_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answer_entity",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_answerentity_student_testentity_TestId",
                        column: x => x.TestId,
                        principalTable: "student_testentity",
                        principalColumn: "StudentTestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "application_user",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "application_user",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_answer_entity_QuestionId",
                table: "answer_entity",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_question_entity_ThreadId",
                table: "question_entity",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_student_answerentity_AnswerId",
                table: "student_answerentity",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_student_answerentity_TestId",
                table: "student_answerentity",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_student_testentity_ApplicationUserId",
                table: "student_testentity",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_student_testentity_StudentId",
                table: "student_testentity",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_testentity_TestId",
                table: "student_testentity",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_test_entity_CreatorId",
                table: "test_entity",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_test_entity_ThreadId",
                table: "test_entity",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_test_questionentity_QuestionId",
                table: "test_questionentity",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_test_questionentity_TestId",
                table: "test_questionentity",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_thread_entity_DisciplineId",
                table: "thread_entity",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "application_role",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_identity_roleclaim<_guid>_RoleId",
                table: "identity_roleclaim<_guid>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_userclaim<_guid>_UserId",
                table: "identity_userclaim<_guid>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_userlogin<_guid>_UserId",
                table: "identity_userlogin<_guid>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_userrole<_guid>_RoleId",
                table: "identity_userrole<_guid>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_userrole<_guid>_UserId",
                table: "identity_userrole<_guid>",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student_answerentity");

            migrationBuilder.DropTable(
                name: "test_questionentity");

            migrationBuilder.DropTable(
                name: "identity_roleclaim<_guid>");

            migrationBuilder.DropTable(
                name: "identity_userclaim<_guid>");

            migrationBuilder.DropTable(
                name: "identity_userlogin<_guid>");

            migrationBuilder.DropTable(
                name: "identity_userrole<_guid>");

            migrationBuilder.DropTable(
                name: "identity_usertoken<_guid>");

            migrationBuilder.DropTable(
                name: "answer_entity");

            migrationBuilder.DropTable(
                name: "student_testentity");

            migrationBuilder.DropTable(
                name: "application_role");

            migrationBuilder.DropTable(
                name: "question_entity");

            migrationBuilder.DropTable(
                name: "test_entity");

            migrationBuilder.DropTable(
                name: "application_user");

            migrationBuilder.DropTable(
                name: "thread_entity");

            migrationBuilder.DropTable(
                name: "discipline_entity");
        }
    }
}
