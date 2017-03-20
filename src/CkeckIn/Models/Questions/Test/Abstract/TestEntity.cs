using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test.Question;
using CkeckIn.Models.Questions.Thread;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CkeckIn.Models.Questions.Test.Abstract
{
    public enum TestFactoryMethod
    {
        Randomize,
        Fixed,
        Strong
    }

    public class TestEntity
    {
        public Guid TestId { get; set; }
        public string TestName { get; set; }

        public Guid ThreadId { get; set; }
        public virtual ThreadEntity ThreadEntity { get; set; }


        public Guid CreatorId { get; set; }
        public virtual ApplicationUser CreatorEntity { get; set; }

        public DateTime CreatedDate { get; set; }
        public int TimetoPerformInSeconds { get; set; }
        public TestFactoryMethod FactoryMethod { get; set; }
        public int QuestionCountLimit { get; set; }

        public virtual List<TestQuestionEntity> QuestionEntities { get; set; } = new List<TestQuestionEntity>(10);
        public IReadOnlyCollection<TestQuestionEntity> ValidQuestionEntities => QuestionEntities.Where(e => e.QuestionEntity.AnswerEntities.Any()).ToArray();
        public int CountQuestionEntities => QuestionCountLimit > QuestionEntities.Count ? QuestionCountLimit : QuestionEntities.Count;

        public ProcessDetailView AsProcessView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<TestEntity, ProcessDetailView>()
                .ForMember(m => m.ThreadName, m => m.MapFrom(r => r.ThreadEntity.ThreadName))
                .ForMember(m => m.DisciplineName, m => m.MapFrom(r => r.ThreadEntity.DisciplineEntity.DisciplineName))
                .ForMember(m => m.DisciplineId, m => m.MapFrom(r => r.ThreadEntity.DisciplineId))
                .ForMember(m => m.CreatorName, m => m.MapFrom(r => r.CreatorEntity.UserName))

            );
            return Mapper.Map<ProcessDetailView>(this);
        }

        //=====================================================================================

        public TestView AsView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<TestEntity, TestView>()
                .ForMember(m => m.ThreadName, m => m.MapFrom(r => r.ThreadEntity.ThreadName))
                .ForMember(m => m.DisciplineName, m => m.MapFrom(r => r.ThreadEntity.DisciplineEntity.DisciplineName))
                .ForMember(m => m.DisciplineId, m => m.MapFrom(r => r.ThreadEntity.DisciplineId))
            );
            return Mapper.Map<TestView>(this);
        }

        public TestDetailView AsDetailView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<TestEntity, TestDetailView>()
                .ForMember(m => m.ThreadName, m => m.MapFrom(r => r.ThreadEntity.ThreadName))
                .ForMember(m => m.DisciplineName, m => m.MapFrom(r => r.ThreadEntity.DisciplineEntity.DisciplineName))
                .ForMember(m => m.DisciplineId, m => m.MapFrom(r => r.ThreadEntity.DisciplineId))
                .ForMember(m => m.QuestionViews, m => m.MapFrom(r => r.QuestionEntities.Select(q => q.AsView()).ToList()))
            );

            return Mapper.Map<TestDetailView>(this);
        }

        public static TestEntity Factory(TestCreateView view, Guid ownerId)
        {
            Mapper.Initialize(c => c.CreateMap<TestCreateView, TestEntity>()
                .ForMember(m => m.TestId, m => m.MapFrom(r => Guid.NewGuid()))
                .ForMember(m => m.CreatedDate, m => m.MapFrom(r => DateTime.UtcNow))
                .ForMember(m => m.CreatorId, m => m.UseValue(ownerId))
            );

            return Mapper.Map<TestEntity>(view);
        }

        //=====================================================================================

        public static void Configuration(EntityTypeBuilder<TestEntity> b)
        {
            b.HasKey(p => p.TestId);
            b.HasOne(p => p.ThreadEntity).WithMany(p => p.TestEntities).HasForeignKey(p => p.ThreadId);
            b.HasOne(p => p.CreatorEntity).WithMany(p => p.CreatedTests).HasForeignKey(p => p.CreatorId);
            b.Ignore(p => p.ValidQuestionEntities);
        }
    }
}
