using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Answer;
using CkeckIn.Models.Questions.Test.Abstract;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CkeckIn.Models.Questions.Student.Test
{
    public class StudentAnswerEntity
    {
        public Guid StudentTestAnswerId { get; set; }

        public Guid TestId { get; set; }
        public virtual StudentTestEntity TestEntity { get; set; }


        public Guid AnswerId { get; set; }
        public virtual AnswerEntity AnswerEntity { get; set; }


        public DateTime AnswedTime { get; set; }

        public static void Configuration(EntityTypeBuilder<StudentAnswerEntity> b)
        {
            b.HasKey(p => p.StudentTestAnswerId);
            b.HasOne(p => p.TestEntity).WithMany(p => p.AnswerEntities).HasForeignKey(p => p.TestId);
            b.HasOne(p => p.AnswerEntity).WithMany().HasForeignKey(p => p.AnswerId);
        }

    }
}
