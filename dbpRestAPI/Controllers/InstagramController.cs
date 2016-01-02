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
        public List<InstagramMediaOptimized> Get()
        {

            //First off we get some details about the user so we can send back some of the counts.
            WebClient webClient = new WebClient();
            var response = webClient.DownloadString("https://api.instagram.com/v1/users/self/?access_token=" + ConfigurationManager.AppSettings["access_token"]);
            UserRootObject deserializedObject = JsonConvert.DeserializeObject<UserRootObject>(response);

            var recentMedia = webClient.DownloadString("https://api.instagram.com/v1/users/self/media/recent/?access_token=" + ConfigurationManager.AppSettings["access_token"]);
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

        [Route("api/GetCachedInstagramMedia")]
        public List<InstagramMediaOptimized> GetCachedInstagramMedia()
        {
            //Even just by accessing this count it will startup and return entries.
            if (BlogEntryCache.CurrentBlogPosts == null)
            {
                //Cold start
                return Get();
            }
            return InstagramCache.CurrentInstagramPosts;
        }

        [Route("api/refreshInstagramCache")]
        public string GetRefreshInstagramCache()
        {
            InstagramCache.CurrentInstagramPosts = this.Get();
            return "Successfully updated the blog cache";
        }
    }
}
