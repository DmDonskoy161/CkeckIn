using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Answer;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CkeckIn.Models.Questions.Test.Question
{
    public class TestQuestionEntity
    {
        public Guid TestQuestionId { get; set; }

        public Guid TestId { get; set; }
        public virtual TestEntity UnitTestEntity { get; set; }


        public Guid QuestionId { get; set; }
        public virtual QuestionEntity QuestionEntity { get; set; }

        public short Position { get; set; }

        public TestQuestionEntity() { }

        public static TestQuestionEntity Factory(TestQuestionCreateView view)
        {
            Mapper.Initialize
            (
                c => c.CreateMap<TestQuestionCreateView, TestQuestionEntity>()
                .ForMember(m => m.TestQuestionId, m => m.MapFrom(r => Guid.NewGuid()))
            );
            return Mapper.Map<TestQuestionEntity>(view);
        }

        public static void Configuration(EntityTypeBuilder<TestQuestionEntity> b)
        {
            b.HasKey(p => p.TestQuestionId);
            b.HasOne(p => p.QuestionEntity).WithMany().HasForeignKey(p => p.QuestionId);
            b.HasOne(p => p.UnitTestEntity).WithMany(p => p.QuestionEntities).HasForeignKey(p => p.TestId);
        }


        public object AsView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<TestQuestionEntity, TestQuestionView>()
                .ForMember(m => m.QuestionMessage, m => m.MapFrom(r => r.QuestionEntity.QuestionMessage))
            );

            return Mapper.Map<TestQuestionView>(this);
        }
    }
}
