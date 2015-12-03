using dbpRestAPI.Cache;
using dbpRestAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Web.Http;

namespace dbpRestAPI.Controllers
{
    public class BlogController : ApiController
    {
        // GET: api/Blog
        [Route("api/getOptimizedBlogs")]
        public List<BlogEntryOptimized> Get()
        {
            //Get entries from my blog API
            using (var client = new WebClient())
            {
                // New code:
                client.BaseAddress = ConfigurationManager.AppSettings["BlogBaseUrl"];
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Accept", "*/*");
                client.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                client.Headers["Content-type"] = "application/json";
                // New code:
                Stream responseData = client.OpenRead("posts?filter[posts_per_page]=4");
                StreamReader reader = new StreamReader(responseData);
                string s = reader.ReadToEnd();

                // deserialize from json
                var response = JsonConvert.DeserializeObject<List<BlogEntry>>(s);
                List<BlogEntryOptimized> optimizedEntries = new List<BlogEntryOptimized>();
                foreach (var item in response)
                {
                    optimizedEntries.Add(new BlogEntryOptimized()
                    {
                        Date = item.date,
                        Snippet = item.excerpt.rendered,
                        Title = item.title.rendered,
                        Url = item.link,
                    });
                }
                return optimizedEntries;
            }
        }

        [Route("api/RefreshBlogCache")]
        public string GetRefreshBlogCache()
        {
            BlogEntryCache.CurrentBlogPosts = this.Get();
            return "Successfully updated the blog cache";
        }

        [Route("api/GetCachedBlogPosts")]
        public List<BlogEntryOptimized> GetCachedBlogPosts()
        {
            //Even just by accessing this count it will startup and return entries.
            if(BlogEntryCache.CurrentBlogPosts == null)
            {
                //Cold start
                return Get();
            }
            return BlogEntryCache.CurrentBlogPosts;
        }

        // GET: api/Blog/5
        [ActionName("Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Blog
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Blog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Blog/5
        public void Delete(int id)
        {
        }
    }
}
