using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Discipline;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Test.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CkeckIn.Models.Questions.Thread
{
    public class ThreadEntity
    {
        public Guid ThreadId { get; set; }
        public string ThreadName { get; set; }

        public Guid DisciplineId { get; set; }
        public virtual DisciplineEntity DisciplineEntity { get; set; }


        public virtual List<QuestionEntity> QuestionEntities { get; set; } = new List<QuestionEntity>(1024);
        public virtual List<TestEntity> TestEntities { get; set; } = new List<TestEntity>(64);


        public ThreadView AsView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<ThreadEntity, ThreadView>()
                .ForMember(m => m.DisciplinedName, m => m.MapFrom(r => r.DisciplineEntity.DisciplineName))
            );
            return Mapper.Map<ThreadView>(this);
        }

        public ThreadDetailView AsDetailView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<ThreadEntity, ThreadDetailView>()
                .ForMember(m => m.DisciplinedName, m => m.MapFrom(r => r.DisciplineEntity.DisciplineName))
            );
            return Mapper.Map<ThreadDetailView>(this);
        }

        public static void Configuration(EntityTypeBuilder<ThreadEntity> b)
        {
            b.HasKey(p => p.ThreadId);
            b.Property(p => p.ThreadName).HasMaxLength(256);
            //b.ToTable("ThreadEntity");
        }

        public static ThreadEntity Factory(ThreadCreateView view)
        {
            Mapper.Initialize(c => c.CreateMap<ThreadCreateView, ThreadEntity>()
                .ForMember(m => m.ThreadId, m => m.MapFrom(r => Guid.NewGuid()))
            );

            return Mapper.Map<ThreadEntity>(view);
        }
    }
}
