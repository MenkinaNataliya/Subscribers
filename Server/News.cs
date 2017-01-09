using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer
{
    public struct Likes
    {
        public int count;
    }

    public struct Comments
    {
        public int count;
    }

    public struct Reposts
    {
        public int count;
    }
    public struct Share
    {
        public int share_count;
    }
    public struct Photo
    {
        public string photo_130;
    }

    public struct Link
    {
        public string url;
    }
    public struct Attachments
    {
        public string type;
        public string photo;
        public string link;
        public string text;
    }
    public class News
    {
        public int owner_id;
        public int from_id;
        public int id;
        public Likes likes;
        public Comments comments;
        public Reposts reposts;
        public double LikesPriority;
        public double CommentsPriority;
        public double RepostsPriority;
        public string text;
        public List<Attachments> attachments;
        //public string photo;
        public Share share;
    }
}
