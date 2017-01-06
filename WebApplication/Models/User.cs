using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class User
    {
        public long Uid { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Deactivated { get; set; }
        public string Photo { get; set; }
        public List<User> Friends { get; set; }
    }
}