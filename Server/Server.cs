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
            //UpdateDb(null);
            // return Translate.DbUsersInUsers(Service.GetMembers());
            Mapper.Initialize(cfg => cfg.CreateMap<Member, User>());
            var tmp = Mapper.Map<List<User>>(Service.GetMembers(group));
            return tmp;
        }

        public static List<News> GetNews(long id)
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<VkApi.VkNews, News>());
            //var tmp = Mapper.Map<List<News>>(VkApi.Service.ParseNews(id));
            return VkApi.Service.ParseNews(id).ConvertAll(new Converter<VkApi.VkNews, News>(VkNewsToServerNews));
        }

        private static News VkNewsToServerNews(VkApi.VkNews news)
        {
            int countShare = 0;
            if (news.attachments != null && news.attachments[0].link.url != null)
            {
                var json = GetJson(news.attachments[0].link.url);

                if (json != null)
                {
                    var jss = new JavaScriptSerializer();

                    var obj = JObject.Parse(json);


                    var tm = jss.Deserialize<Share>(obj.ToString());
                    countShare = tm.share_count;
                }
                
            }

            return new News
            {
                likes = new Likes { count = news.likes.count },
                comments = new Comments { count = news.comments.count },
                reposts = new Reposts { count = news.reposts.count },
                text = news.text,
                photo = (news.attachments == null) ? "" : news.attachments[0].photo.photo_130,
                share = new Share { share_count = countShare }
               // link = (news.attachments == null) ? "" : ((news.attachments[0].link.url==null) ? "": news.attachments[0].link.url)
            };

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
