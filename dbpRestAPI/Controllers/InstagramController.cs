using dbpRestAPI.Cache;
using dbpRestAPI.Models;
using InstaSharp;
using InstaSharp.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static dbpRestAPI.Cache.InstagramCache;

namespace dbpRestAPI.Controllers
{
    public class InstagramController : ApiController
    {
        // GET: api/Instagram
        [Route("api/GetInstagramMedia")]
        public List<InstagramMediaOptimized> Get()
        {
            WebClient webClient = new WebClient();

            var recentMedia = webClient.DownloadString("https://api.instagram.com/v1/users/self/media/recent/?count=12&access_token=" + ConfigurationManager.AppSettings["access_token"]);
            MediaRootObject recentMediaDeserializedObject = JsonConvert.DeserializeObject<MediaRootObject>(recentMedia);

            List<InstagramMediaOptimized> recentPosts = new List<InstagramMediaOptimized>();
            foreach (var item in recentMediaDeserializedObject.data)
            {
                recentPosts.Add(new InstagramMediaOptimized()
                {
                    Caption = item.caption.text,
                    Link = item.link,
                    ImageURL = item.images.low_resolution.url
                });
            }

            return recentPosts;
        }

        [Route("api/GetInstagramUserData")]
        public InstagramUserDataOptimized GetInstagramUserData()
        {
            WebClient webClient = new WebClient();
            var response = webClient.DownloadString("https://api.instagram.com/v1/users/self/?access_token=" + ConfigurationManager.AppSettings["access_token"]);
            UserRootObject deserializedObject = JsonConvert.DeserializeObject<UserRootObject>(response);

            return new InstagramUserDataOptimized()
            {
                FollowersCount = deserializedObject.data.counts.followed_by.ToString(),
                PostsCount = deserializedObject.data.counts.media.ToString(),
                ProfileImageUrl = deserializedObject.data.profile_picture,
                Username = deserializedObject.data.username,
                FollowingCount = deserializedObject.data.counts.follows.ToString()
            };
        }


        [Route("api/GetCachedInstagramMedia")]
        public List<InstagramMediaOptimized> GetCachedInstagramMedia()
        {
            //Even just by accessing this count it will startup and return entries.
            if (InstagramCache.CurrentInstagramPosts == null)
            {
                //Cold start
                return this.Get();
            }

            return InstagramCache.CurrentInstagramPosts;
        }

        [Route("api/GetCachedInstagramUserData")]
        public InstagramUserDataOptimized GetCachedInstagramUserData()
        {
            //Even just by accessing this count it will startup and return entries.
            if (InstagramCache.CurrentUserData == null)
            {
                //Cold start
                return this.GetInstagramUserData();
            }

            return InstagramCache.CurrentUserData;
        }


        [Route("api/refreshInstagramCache")]
        public string GetRefreshInstagramCache()
        {
            InstagramCache.CurrentInstagramPosts = this.Get();
            return "Successfully updated the instagram cache";
        }
    }
}
