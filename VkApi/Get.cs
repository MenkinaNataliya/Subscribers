using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace VkApi
{
    public class Get
    {
        public static string Members(string group)
        {
            return GetJson("groups.getMembers?group_id="+group+ "&fields=1,photo_200_orig,deactivated");
        }
        public static string UsersById(string ids)
        {
            return GetJson("users.get?user_ids=" + ids + "&fields=photo_200_orig,deactivated");
        }


        public static string Friends(long id)
        {
            return GetJson("friends.get?user_id=" + id + "&fields=1");
        }

        public static string VkNews(long id)
        {
            return GetJson("wall.get?owner_id=" + id + "&filter=owner&count=50&offset=0");
        }

        private static string RemoveRoot(string json)
        {
            var obj = JObject.Parse(json);
            var token = obj["response"];
            return token?.ToString();
        }

        private static string GetJson(string query)
        {
            WebRequest req = WebRequest.Create("https://api.vk.com/method/" + query + "&v=5.57");
            WebResponse resp = req.GetResponse();
            StreamReader str = new StreamReader(resp.GetResponseStream());
            return RemoveRoot(str.ReadToEnd());
        }
    }
}
