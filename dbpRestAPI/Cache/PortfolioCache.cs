using dbpRestAPI.Controllers;
using dbpRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace dbpRestAPI.Cache
{
    public static class PortfolioCache
    {
        public static List<PortfolioBook> CurrentPortfolioBooks { get; set; }
        public static List<PortfolioCategory> CurrentPortfolioCategories { get; set; }

        static PortfolioCache()
        {
            ThreadPool.QueueUserWorkItem(RefreshPortfolioCache);
        }

        public static void RefreshPortfolioCache(object data)
        {
            PortfolioBooksController controller = new PortfolioBooksController();

            CurrentPortfolioBooks = controller.GetPortfolioBooksForDisplay().ToList();
            CurrentPortfolioCategories = controller.GetPortfolioCategories().ToList();
        }
    }
}