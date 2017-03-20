using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CkeckIn.Models
{
    public class NavigationCrud
    {
        public NavigationCrud(string title, string controller, string icon = "")
        {
            Controller = controller;
            Title = title;
            Icon = icon;
        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
    }
}
