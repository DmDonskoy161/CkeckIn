using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Question;
using CkeckIn.Models.Questions.Thread;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CkeckIn.Models.Questions.Discipline
{
    public enum DisciplineCourse : byte
    {
        All,
        C1 = 1,
        C2 = 2,
        C3 = 4,
        C4 = 8,
        C5 = 16,
        C6 = 32
    }

    public class DisciplineEntity
    {
        public Guid DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public DisciplineCourse Course { get; set; }

        public virtual List<ThreadEntity> ThreadEntities { get; set; } = new List<ThreadEntity>(64);

        public DisciplineView AsView()
        {
            Mapper.Initialize(c => 
                c.CreateMap<DisciplineEntity, DisciplineView>()
                .ForMember(m => m.ThreadViews, m => m.MapFrom(r => r.ThreadEntities.Take(5).Select(d => d.AsView()).ToList()))
            );
            return Mapper.Map<DisciplineView>(this);
        }

        public DisciplineDetailView AsDetailView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<DisciplineEntity, DisciplineDetailView>()
                .ForMember(m => m.ThreadViews, m => m.MapFrom(r => r.ThreadEntities.Select(d => d.AsView()).ToList()))
            );
            return Mapper.Map<DisciplineDetailView>(this);
        }

        public static void Configuration(EntityTypeBuilder<DisciplineEntity> b)
        {
            b.HasKey(p => p.DisciplineId);
            b.Property(p => p.DisciplineName).HasMaxLength(128);
            b.HasMany(p => p.ThreadEntities).WithOne(p => p.DisciplineEntity).HasForeignKey(p => p.DisciplineId);
        }


        public static DisciplineEntity Factory(DisciplineCreateView view)
        {
            Mapper.Initialize(c => c.CreateMap<DisciplineCreateView, DisciplineEntity>()
                .ForMember(m => m.DisciplineId, m => m.MapFrom(r => Guid.NewGuid()))
                .ForMember(m => m.ThreadEntities, m => m.Ignore())
            );

            return Mapper.Map<DisciplineEntity>(view);
        }
    }
}
