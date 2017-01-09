using AutoMapper;
using Db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace AppServer
{
    public class Server
    {
        public static List<User> GetMembers( string group)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Member, User>());
            return Mapper.Map<List<User>>(Service.GetMembers(group));
        }

        public static List<News> GetNews(long id)
        {
            var news  = VkApi.Service.ParseNews(id).ConvertAll(new Converter<VkApi.VkNews, News>(Translate.VkNewsToServerNews));
            return news;
           
        }

        public virtual string GetNameById(int id)
        {
            var user = Service.GetUserById(id);
            return user.FirstName + " " + user.SecondName;
        }



        public  virtual int CountShare(List<VkApi.Attachments> attachments)
        {
            int countShare = 0;
            foreach (var attach in attachments)
            {
                if (attach.link.url != null)
                {
                    var json = GetJson(attach.link.url);
                    if (json != null)
                    {
                        var jss = new JavaScriptSerializer();
                        var obj = JObject.Parse(json);
                        var tm = jss.Deserialize<Share>(obj.ToString());
                        countShare += tm.share_count;
                    }
                }
            }
            return countShare;
        }


        public static long CalculateCoefficient(List<News> news)
        {
            int Amount = 0;
            

            foreach (var item in news)
                Amount += item.likes.count + item.comments.count + item.reposts.count;

            return Amount / news.Count;

        }

        private static string RemoveRoot(string json)
        {
            var obj = JObject.Parse(json);
            var token = obj["og_object"];
            return token?.ToString();
        }

        private static string GetJson(string query)
        {
            WebRequest req = WebRequest.Create("http://graph.facebook.com/?id=" + query );
            WebResponse resp = req.GetResponse();
            StreamReader str = new StreamReader(resp.GetResponseStream());
            return RemoveRoot(str.ReadToEnd());
        }
  


    }
}
