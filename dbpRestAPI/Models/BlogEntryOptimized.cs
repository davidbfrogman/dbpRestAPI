using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class BlogEntryOptimized
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Snippet { get; set; }
        public string Url { get; set; }
    }
}