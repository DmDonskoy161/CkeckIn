using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CkeckIn.Models.Questions.Test
{
    public class ProcessTestCompleteView
    {
        public ProcessDetailView TestDetailView { get; set; }
        public long CountOfTotalAnswers { get; set; }
        public long CountOfCorrectAnswers { get; set; }
        public long Mark { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
