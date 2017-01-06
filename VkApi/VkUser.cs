using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi
{
    public class VkUser
    {
        public List<VkUser> Friends;

        public long id;
        public string first_name;
        public string last_name;
        public string photo_200_orig;
        public string deactivated = "";

    }
}
