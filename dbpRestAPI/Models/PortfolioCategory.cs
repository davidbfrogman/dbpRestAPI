using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class PortfolioCategory
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string Filter { get; set; }
        public int Order { get; set; }
    }
}