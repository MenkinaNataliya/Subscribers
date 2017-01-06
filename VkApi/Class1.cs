using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi
{
    public class Controller
    {
        private static List<VkUser> users;
        //private static List<VkNews> news;

        public static List<VkNews> GetVkNewsByIdFriends(long id)
        {
            //news = Service.ParseNews(Get.VkNews(id));
            //return SortingNews(news);

            return Service.ParseNews(Get.VkNews(id));
        }


        //public static List<VkUser> GetGroupMembers()
        //{
        //    users = Service.ParseUsers(Get.Members());
        //    return users;
        //}

        public static List<VkNews> SortingNews(List<VkNews> news)
        {
            var sortCollection = new List<VkNews>();

            foreach (var item in news)
            {
                if (item.comments.count == 0) sortCollection.Add(item);
                else
                {
                    int index = sortCollection.FindIndex(x => x.comments.count == item.comments.count);
                    if (index == -1) sortCollection.Insert(0, item);
                    else sortCollection.Insert(index, item);
                }
                //sortCollection.Insert(sortCollection.FindIndex(x => x.comments.count == item.comments.count), item);

            }

            return sortCollection;
        }
    }
}
