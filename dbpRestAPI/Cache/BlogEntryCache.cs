using dbpRestAPI.Controllers;
using dbpRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace dbpRestAPI.Cache
{
    public static class BlogEntryCache
    {
        public static List<BlogEntryOptimized> CurrentBlogPosts { get; set; }

        static BlogEntryCache()
        {
            ThreadPool.QueueUserWorkItem(RefreshBlogCache);
        }

        public static void RefreshBlogCache(object data)
        {
            BlogController controller = new BlogController();

            CurrentBlogPosts = controller.Get();
        }
    }
}