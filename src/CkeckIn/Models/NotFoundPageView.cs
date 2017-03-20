using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CkeckIn.Models
{
    public class NotFoundPageView
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Message { get; set; }
        public string Title { get;set ;}
        public string ReturnUrl { get; set; }
        public SelectList EntitySelectList { get; set; }
    }
}
