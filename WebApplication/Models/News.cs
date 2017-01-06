using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class News
    {
        public int owner_id { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public int likes { get; set; }
        public int comments { get; set; }
        public int reposts { get; set; }
        public double date { get; set; }
        public string text { get; set; }
        public string photo { get; set; }
    }
}