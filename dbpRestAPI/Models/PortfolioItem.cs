using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class PortfolioItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public int Order { get; set; }
    }
}