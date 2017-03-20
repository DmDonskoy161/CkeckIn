using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Models.Questions.Student.Test;
using CkeckIn.Models.Questions.Test.Abstract;
using CkeckIn.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CkeckIn.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public long? RecordBookId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? ActiveTestId { get; set; }
        public virtual StudentTestEntity ActiveTestEntity { get; set; }

        public virtual List<StudentTestEntity> PerformedTests { get; set; } = new List<StudentTestEntity>(32);
        public virtual List<TestEntity> CreatedTests { get; set; } = new List<TestEntity>(32);
    }
}
