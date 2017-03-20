using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CkeckIn.Models;
using CkeckIn.Models.Questions.Answer;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Student.Test;
using CkeckIn.Models.Questions.Teacher;
using CkeckIn.Models.Questions.Test.Abstract;
using CkeckIn.Models.Questions.Test.Question;
using CkeckIn.Models.Questions.Thread;
using CkeckIn.Models.User;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CkeckIn.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(/*DbContextOptions<ApplicationDbContext> options*/)
            : base(/*options*/)
        {
        }

        public virtual DbSet<AnswerEntity> AnswerEntities { get; set; }
        public virtual DbSet<QuestionEntity> QuestionEntities { get; set; }

        public virtual DbSet<DisciplineEntity> DisciplineEntities { get; set; }
        public virtual DbSet<ThreadEntity> ThreadEntities { get; set; }


        //==============================================================================
        public virtual DbSet<TestEntity> TestEntities { get; set; }
        public virtual DbSet<TestQuestionEntity> TestQuestionEntities { get; set; }

        public virtual DbSet<StudentTestEntity> StudentTestEntities { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=ApplicationDbContext.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasOne(p => p.ActiveTestEntity).WithMany().HasForeignKey(p => p.ActiveTestId);

            //================================================================
            builder.Entity<AnswerEntity>(AnswerEntity.Configuration);

            //================================================================
            builder.Entity<DisciplineEntity>(DisciplineEntity.Configuration);

            //================================================================
            builder.Entity<QuestionEntity>(QuestionEntity.Configuration);

            //================================================================

            //================================================================
            builder.Entity<StudentAnswerEntity>(StudentAnswerEntity.Configuration);
            
            //================================================================
            builder.Entity<StudentTestEntity>(StudentTestEntity.Configuration);

            //================================================================
            //builder.Entity<TeacherAttachedDisciplineEntity>(TeacherAttachedDisciplineEntity.Configuration);

            //================================================================
            builder.Entity<TestEntity>(TestEntity.Configuration);

            //================================================================
            builder.Entity<TestQuestionEntity>(TestQuestionEntity.Configuration);

            //================================================================
            builder.Entity<ThreadEntity>(ThreadEntity.Configuration);

            builder.SetSimpleUnderscoreTableNameConvention();
        }
    }
    public static class ModelBuilderExtensions
    {
        public static void SetSimpleUnderscoreTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                Regex underscoreRegex = new Regex(@"((?<=.)[A-Z][a-zA-Z]*)|((?<=[a-zA-Z])\d+)");
                entity.Relational().TableName = underscoreRegex.Replace(entity.DisplayName(), @"_$1$2").ToLower();
            }
        }
    }
}
