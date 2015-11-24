using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class PortfolioBook
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}