using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkApi
{
    public class Service
    {
        public static List<VkNews> ParseNews(long id)
        {
            string json = Get.VkNews(id);
            if (json == null) return new List<VkNews>();
            var jss = new JavaScriptSerializer();

            var obj = JObject.Parse(json);
            var ListObj = new List<object>(obj["items"]);

            List<VkNews> collection = new List<VkNews>();

            foreach (var dop in ListObj)
            {
                
                var post = jss.Deserialize<VkNews>(dop.ToString());
                collection.Add(post);
            }
            return collection;
        }


        public static string ParseIds(string json)
        {
            var id = new List<long>();
            if (json != null)
            {
                var obj = JObject.Parse(json);
                var token = obj["items"];
                var listUser = token.ToList();

                foreach (var user in listUser)
                    id.Add((long)user);
            }
            string ids = "";
            foreach (var i in id)
                ids += i + ",";
            return ids;
        }

        private static List<VkUser> DeserializeVkUser(string json)
        {
            var obj = JObject.Parse(json);
            var token = obj["items"];
            var listUser = token.ToString();
            return JsonConvert.DeserializeObject<List<VkUser>>(listUser);
        }



        public static List<VkUser> ParseUsers(string group)
        {
            var ids = Get.Members(group);

            var users = new List<VkUser>();
            if (ids != null)
            {
                users = DeserializeVkUser(ids);
                foreach (var user in users)
                {
                    var jsonFriends = Get.Friends(user.id);
                    if (jsonFriends != null)
                        user.Friends = DeserializeVkUser(jsonFriends);
                    else
                        user.Friends = null;
                }
            }
            return users;
        }

       
    }
}
