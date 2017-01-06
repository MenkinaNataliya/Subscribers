using AutoMapper;
using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new News
            {
                likes = new Likes { count = news.likes.count },
                comments = new Comments { count = news.comments.count },
                reposts = new Reposts { count = news.reposts.count },
                text = news.text,
                photo = (news.attachments == null) ? "" : news.attachments[0].photo.photo_130
            };

        }


    }
}
