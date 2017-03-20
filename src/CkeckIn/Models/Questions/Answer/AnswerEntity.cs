using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test;
using CkeckIn.Models.Questions.Thread;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CkeckIn.Models.Questions.Answer
{
    public class AnswerEntity
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }

        public string AnswerMessage { get; set; }
        public short Mark { get; set; }

        public virtual QuestionEntity QuestionEntity { get; set; }

        public AnswerView AsView()
        {
            Mapper.Initialize(c =>
                c.CreateMap<AnswerEntity, AnswerView>()
            );
            return Mapper.Map<AnswerView>(this);
        }

        public AnswerDetailView AsDetailView()
        {
            Mapper.Initialize(c =>
                c.CreateMap<AnswerEntity, AnswerDetailView>()
            );
            return Mapper.Map<AnswerDetailView>(this);
        }

        public static void Configuration(EntityTypeBuilder<AnswerEntity> b)
        {
            b.HasKey(p => p.AnswerId);
            b.Property(p => p.AnswerMessage).HasMaxLength(256);
        }

        public static AnswerEntity Factory(AnswerCreateView view)
        {
            Mapper.Initialize(c =>
                c.CreateMap<AnswerCreateView, AnswerEntity>()
                .ForMember(m => m.AnswerId, m => m.MapFrom(r => Guid.NewGuid()))
            );

            return Mapper.Map<AnswerEntity>(view);
        }
    }
}
