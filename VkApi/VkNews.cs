﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi
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
    public struct Photo
    {
        public string photo_130;
    }

    public struct Attachments
    {
        public string type;
        public Photo photo;
    }

    public class VkNews
    {
        public int owner_id;
        public int from_id;
        public int id;
        public Likes likes;
        public Comments comments;
        public Reposts reposts;
        public double date;
        public string text;
       // public VkNews copy_history;
        public List<Attachments> attachments;


    }
}
