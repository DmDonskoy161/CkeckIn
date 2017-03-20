using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Answer;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test;
using CkeckIn.Models.Questions.Test.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CkeckIn.Models.Questions.Student.Test
{
    public class StudentTestEntity
    {

        public Guid StudentTestId { get; set; }

        public Guid StudentId { get; set; }
        public virtual ApplicationUser StudentEntity { get; set; }

        public Guid TestId { get; set; }
        public virtual TestEntity TestEntity { get; set; }

        public DateTime StartedDateTime { get; set; }
        public DateTime CompleteDateTime { get; set; }

        public virtual List<StudentAnswerEntity> AnswerEntities { get; set; } = new List<StudentAnswerEntity>(16);
        public int ValidAnswers { get; set; }
        public int NumberAnswers { get; set; }

        public QuestionEntity TakeQuestion()
        {
            var answed = AnswerEntities.Select(a => a.AnswerEntity.QuestionId).ToList();

            if (TestEntity.FactoryMethod == TestFactoryMethod.Fixed)
            {
                return TestEntity.ValidQuestionEntities.OrderBy(q => q.Position).FirstOrDefault(q => answed.Contains(q.QuestionId) == false)?.QuestionEntity;
            }
            else if (TestEntity.FactoryMethod == TestFactoryMethod.Randomize)
            {
                return TestEntity.ValidQuestionEntities.FirstOrDefault(q => answed.Contains(q.QuestionId) == false)?.QuestionEntity;
            }
            else
            {
                throw new NotImplementedException();
            }
            return null;
        }

        public ProcessTestCompleteView AsCompleteView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<StudentTestEntity, ProcessTestCompleteView>()
                .ForMember(m => m.TestDetailView, m => m.MapFrom(r => TestEntity.AsProcessView()))
                .ForMember(m => m.CountOfTotalAnswers, m => m.MapFrom(r => r.NumberAnswers))
                .ForMember(m => m.CountOfCorrectAnswers, m => m.MapFrom(r => r.ValidAnswers))
                .ForMember(m => m.BeginDate, m => m.MapFrom(r => r.StartedDateTime))
                .ForMember(m => m.EndDate, m => m.MapFrom(r => r.CompleteDateTime))
            );

            return Mapper.Map<ProcessTestCompleteView>(this);
        }

        public ProcessTestCompleteView CompleteTest()
        {
            CompleteDateTime = DateTime.UtcNow;
            ValidAnswers = AnswerEntities.Count(a => a.AnswerEntity.Mark > 0);
            NumberAnswers = AnswerEntities.Count();

            return AsCompleteView();
        }

        public ProcessTestWhileView TakeWhileView()
        {
            var question = TakeQuestion();

            Mapper.Initialize
            (
                c => c.CreateMap<StudentTestEntity, ProcessTestWhileView>()
                .ForMember(m => m.TestDetailView, m => m.MapFrom(r => TestEntity.AsProcessView()))
                .ForMember(m => m.CountOfQuestions, m => m.MapFrom(r => r.TestEntity.CountQuestionEntities))
                .ForMember(m => m.CurrentOfQuestions, m => m.MapFrom(r => r.NumberAnswers))
                .ForMember(m => m.QuestionName, m => m.MapFrom(r => question.QuestionMessage))
                .ForMember(m => m.AnswerModel, m => m.MapFrom(r => new ProcessAnswerModel
                {
                    AnswerViews = new SelectList(question.AnswerEntities.ToList(), "AnswerId", "AnswerMessage")
                }))
            );

            return Mapper.Map<ProcessTestWhileView>(this);
        }

        public static void Configuration(EntityTypeBuilder<StudentTestEntity> b)
        {
            b.HasKey(p => p.StudentTestId);
            b.HasOne(p => p.StudentEntity).WithMany(p => p.PerformedTests).HasForeignKey(p => p.StudentId);
            b.HasOne(p => p.TestEntity).WithMany().HasForeignKey(p => p.TestId);
            b.HasMany(p => p.AnswerEntities).WithOne(p => p.TestEntity).HasForeignKey(p => p.TestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
