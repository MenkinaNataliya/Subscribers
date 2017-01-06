using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer
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
