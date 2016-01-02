using dbpRestAPI.Controllers;
using dbpRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace dbpRestAPI.Cache
{
    public static class InstagramCache
    {
        public static List<InstagramMediaOptimized> CurrentInstagramPosts { get; set; }

        static InstagramCache()
        {
            ThreadPool.QueueUserWorkItem(RefreshInstagramCache);
        }

        public static void RefreshInstagramCache(object data)
        {
            InstagramController controller = new InstagramController();

            CurrentInstagramPosts = controller.Get();
        }
    }
}