using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class InstagramUserDataOptimized
    {
        public string Username { get; set; }
        public string ProfileImageUrl { get; set; }
        public string FollowersCount { get; set; }
        public string PostsCount { get; set; }
        public string FollowingCount { get; set; }
    }
}