using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer
{
    public class Translate
    {
        public static Db.Member VkUserToDbUser(VkApi.VkUser user)
        {
            return new Db.Member
            {
                Uid = user.id,
                FirstName = user.first_name,
                Deactivated = user.deactivated,
                SecondName = user.last_name,
                Photo = user.photo_200_orig,
                Friends = (user.Friends == null) ? null : user.Friends.ConvertAll(new Converter<VkApi.VkUser, Db.Member>(VkUserToDbUser))
            };
        }
        public static News VkNewsToServerNews(VkApi.VkNews news)
        {
            return new News
            {
                likes = new Likes { count = news.likes.count },
                comments = new Comments { count = news.comments.count },
                reposts = new Reposts { count = news.reposts.count },
                text =  news.text,
               // photo = (news.attachments == null) ? "" : news.attachments[0].photo.photo_130,
                share = new Share { share_count = ((news.attachments == null) ? 0 : Server.CountShare(news.attachments)) },
                attachments = (news.attachments == null) ?
                            CopyHistoryToAttachment(news.copy_history) : 
                            news.attachments.ConvertAll(new Converter<VkApi.Attachments, Attachments>(VkAttachmentToAttachment))
            };

        }

        public static Attachments VkAttachmentToAttachment(VkApi.Attachments attach)
        {
            return new Attachments
            {
                photo = attach.photo.photo_1280,
                link = attach.link.url
            };
        }

        public static List<Attachments> CopyHistoryToAttachment(List<VkApi.CopyHistory> copyHistory)
        {
            if(copyHistory == null ) return new List<Attachments>(); 
            var collectionAttach = new List<Attachments>();
            foreach(var copy in copyHistory)
            {
                if(copy.attachments != null)
                    foreach(var attach in copy.attachments)
                    {
                        collectionAttach.Add(VkAttachmentToAttachment(attach));
                    }
                else
                    collectionAttach.Add(new Attachments { text = copy.text});
            }
            return collectionAttach;
            
        }



    }
}
