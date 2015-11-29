using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class dbpEmail
    {
        public string FromAddress { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Budget { get; set; }
    }
}