using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CkeckIn.Models.Questions.Answer;
using CkeckIn.Models.Questions.Thread;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CkeckIn.Models.Questions.Question
{
    public class QuestionEntity
    {
        public Guid QuestionId { get; set; }
        public string QuestionMessage { get; set; }
        public short Strong { get; set; }

        public Guid RepositoryId { get; set; }

        //------------------------------------------------------------------

        public Guid CreatorId { get; set; }
        public virtual ApplicationUser CreatorEntity { get; set; }

        //------------------------------------------------------------------

        #region Discipline
        public Guid ThreadId { get; set; }
        public virtual ThreadEntity ThreadEntity { get; set; }
        #endregion

        //------------------------------------------------------------------

        public virtual List<AnswerEntity> AnswerEntities { get; set; } = new List<AnswerEntity>(4);
        public int NumberOfAnswers => AnswerEntities.Count;
        public QuestionView AsView()
        {
            Mapper.Initialize
            (
                c => c.CreateMap<QuestionEntity, QuestionView>()
                .ForMember(m => m.ThreadName, m => m.MapFrom(r => r.ThreadEntity.ThreadName))
                .ForMember(m => m.DisciplineName, m => m.MapFrom(r => r.ThreadEntity.DisciplineEntity.DisciplineName))

            );
            return Mapper.Map<QuestionView>(this);
        }

        public QuestionDetailView AsDetailView()
        {
            Mapper.Initialize
                (
                    c => c.CreateMap<QuestionEntity, QuestionDetailView>()
                    .ForMember(m => m.ThreadName, m => m.MapFrom(r => r.ThreadEntity.ThreadName))
                    .ForMember(m => m.DisciplineName, m => m.MapFrom(r => r.ThreadEntity.DisciplineEntity.DisciplineName))
                    .ForMember(m => m.AnswerViews, m => m.MapFrom(r => r.AnswerEntities.Select(d => d.AsView()).ToList()))
                );
            return Mapper.Map<QuestionDetailView>(this);
        }

        public static QuestionEntity Factory(QuestionCreateView view, Guid creatorId)
        {
            Mapper.Initialize
            (
                c => c.CreateMap<QuestionCreateView, QuestionEntity>()
                .ForMember(m => m.QuestionId, m => m.MapFrom(r => Guid.NewGuid()))
                .ForMember(m => m.CreatorId, m => m.UseValue(creatorId))
            );
            return Mapper.Map<QuestionEntity>(view);
        }

        public static void Configuration(EntityTypeBuilder<QuestionEntity> b)
        {
            b.HasKey(p => p.QuestionId);
            b.Property(p => p.QuestionMessage).HasMaxLength(1024);

            b.HasMany(p => p.AnswerEntities).WithOne(p => p.QuestionEntity).HasForeignKey(p => p.QuestionId);
            b.HasOne(p => p.ThreadEntity).WithMany(p => p.QuestionEntities).HasForeignKey(p => p.ThreadId);
            b.HasOne(p => p.CreatorEntity).WithMany().HasForeignKey(p => p.CreatorId);

            
            //b.HasOne(p => p.CreatorEntity).WithMany().HasForeignKey(p => p.CreatorId);
        }
    }
}
