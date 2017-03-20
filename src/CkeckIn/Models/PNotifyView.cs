using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CkeckIn.Models
{
    public enum PNotifyStatus
    {
        Dark,
        Success,
        Info,
        Error,
    }

    public class PNotifyView
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public PNotifyStatus Status { get; set; }
    }
}
