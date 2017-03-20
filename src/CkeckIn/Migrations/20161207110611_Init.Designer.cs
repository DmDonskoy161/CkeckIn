using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CkeckIn.Data;

namespace CkeckIn.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161207110611_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("CkeckIn.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("application_user");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Answer.AnswerEntity", b =>
                {
                    b.Property<Guid>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerMessage")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<short>("Mark");

                    b.Property<Guid>("QuestionId");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("answer_entity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Discipline.DisciplineEntity", b =>
                {
                    b.Property<Guid>("DisciplineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisciplineName")
                        .HasAnnotation("MaxLength", 128);

                    b.HasKey("DisciplineId");

                    b.ToTable("discipline_entity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Question.QuestionEntity", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatorId");

                    b.Property<string>("QuestionMessage")
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<Guid>("RepositoryId");

                    b.Property<short>("Strong");

                    b.Property<Guid>("ThreadId");

                    b.HasKey("QuestionId");

                    b.HasIndex("ThreadId");

                    b.ToTable("question_entity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Student.Test.StudentAnswerEntity", b =>
                {
                    b.Property<Guid>("StudentTestAnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AnswedTime");

                    b.Property<Guid>("AnswerId");

                    b.Property<Guid>("TestId");

                    b.HasKey("StudentTestAnswerId");

                    b.HasIndex("AnswerId");

                    b.HasIndex("TestId");

                    b.ToTable("student_answerentity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Student.Test.StudentTestEntity", b =>
                {
                    b.Property<Guid>("StudentTestId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<DateTime>("CompleteDateTime");

                    b.Property<int>("NumberAnswers");

                    b.Property<DateTime>("StartedDateTime");

                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("TestId");

                    b.Property<int>("ValidAnswers");

                    b.HasKey("StudentTestId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TestId");

                    b.ToTable("student_testentity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Test.Abstract.TestEntity", b =>
                {
                    b.Property<Guid>("TestId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("CreatorId");

                    b.Property<int>("FactoryMethod");

                    b.Property<int>("QuestionCountLimit");

                    b.Property<Guid>("ThreadId");

                    b.Property<int>("TimetoPerformInSeconds");

                    b.HasKey("TestId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ThreadId");

                    b.ToTable("test_entity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Test.Question.TestQuestionEntity", b =>
                {
                    b.Property<Guid>("TestQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Position");

                    b.Property<Guid>("QuestionId");

                    b.Property<Guid>("TestId");

                    b.HasKey("TestQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestId");

                    b.ToTable("test_questionentity");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Thread.ThreadEntity", b =>
                {
                    b.Property<Guid>("ThreadId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DisciplineId");

                    b.Property<string>("ThreadName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("ThreadId");

                    b.HasIndex("DisciplineId");

                    b.ToTable("thread_entity");
                });

            modelBuilder.Entity("CkeckIn.Models.User.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("application_role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("identity_roleclaim<_guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("identity_userclaim<_guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("identity_userlogin<_guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("identity_userrole<_guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("identity_usertoken<_guid>");
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Answer.AnswerEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.Questions.Question.QuestionEntity", "QuestionEntity")
                        .WithMany("AnswerEntities")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Question.QuestionEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.Questions.Thread.ThreadEntity", "ThreadEntity")
                        .WithMany("QuestionEntities")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Student.Test.StudentAnswerEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.Questions.Answer.AnswerEntity", "AnswerEntity")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CkeckIn.Models.Questions.Student.Test.StudentTestEntity", "TestEntity")
                        .WithMany("AnswerEntities")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Student.Test.StudentTestEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.ApplicationUser")
                        .WithMany("PerformedTests")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("CkeckIn.Models.ApplicationUser", "StudentEntity")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CkeckIn.Models.Questions.Test.Abstract.TestEntity", "TestEntity")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Test.Abstract.TestEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.ApplicationUser", "CreatorEntity")
                        .WithMany("CreatedTests")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CkeckIn.Models.Questions.Thread.ThreadEntity", "ThreadEntity")
                        .WithMany("TestEntities")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Test.Question.TestQuestionEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.Questions.Question.QuestionEntity", "QuestionEntity")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CkeckIn.Models.Questions.Test.Abstract.TestEntity", "UnitTestEntity")
                        .WithMany("QuestionEntities")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CkeckIn.Models.Questions.Thread.ThreadEntity", b =>
                {
                    b.HasOne("CkeckIn.Models.Questions.Discipline.DisciplineEntity", "DisciplineEntity")
                        .WithMany("ThreadEntities")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CkeckIn.Models.User.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CkeckIn.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CkeckIn.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CkeckIn.Models.User.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CkeckIn.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
