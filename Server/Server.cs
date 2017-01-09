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
            return NormalizedUserPosts(news, Service.CountNumberFriends(id));
           
        }

        public static string GetNameById(int id)
        {
            var user = Service.GetUserById(id);
            return user.FirstName + " " + user.SecondName;
        }



        public  static int CountShare(List<VkApi.Attachments> attachments)
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


        private static List<News> NormalizedUserPosts(List<News> news, int countFriends)
        {
            int likesAmount = 0;
            int commentsAmount = 0;
            int repostsAmount = 0;
            foreach (var item in news)
            {
                likesAmount += item.likes.count;
                commentsAmount += item.comments.count;
                repostsAmount += item.reposts.count;
            }

            foreach (var item in news)
            {
                item.LikesPriority = item.likes.count / ((double)likesAmount / news.Count);
                item.CommentsPriority = item.comments.count / ((double)commentsAmount / news.Count);
                item.RepostsPriority = item.reposts.count / ((double)repostsAmount / news.Count);
            }

            return news;
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
