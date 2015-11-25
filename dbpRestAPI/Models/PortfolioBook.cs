using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class PortfolioBook
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageThumbnailURL { get; set; }
        public int Order { get; set; }
        public string Category { get; set; }
        List<PortfolioItem> Items { get; set; }
    }
}