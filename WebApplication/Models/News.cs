using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class News
    {
        public int likes { get; set; }
        public int comments { get; set; }
        public double LikesPriority { get; set; }
        public double CommentsPriority { get; set; }
        public double RepostsPriority { get; set; }
        public int reposts { get; set; }
        public int shares { get; set; }
        public string text { get; set; }
        public string photo { get; set; }

       
    }
}