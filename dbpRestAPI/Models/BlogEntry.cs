﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class BlogEntry
    {
        public int id { get; set; }
        public string date { get; set; }
        public string date_gmt { get; set; }
        public Guid guid { get; set; }
        public string modified { get; set; }
        public string modified_gmt { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public Title title { get; set; }
        public Content content { get; set; }
        public Excerpt excerpt { get; set; }
        public int author { get; set; }
        public int featured_image { get; set; }
        public string comment_status { get; set; }
        public string ping_status { get; set; }
        public bool sticky { get; set; }
        public string format { get; set; }
        public Links _links { get; set; }

        public class Guid
        {
            public string rendered { get; set; }
        }

        public class Title
        {
            public string rendered { get; set; }
        }

        public class Content
        {
            public string rendered { get; set; }
        }

        public class Excerpt
        {
            public string rendered { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Collection
        {
            public string href { get; set; }
        }

        public class Author
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class Reply
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class VersionHistory
        {
            public string href { get; set; }
        }

        public class HttpApiWOrgFeaturedmedia
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class HttpApiWOrgAttachment
        {
            public string href { get; set; }
        }

        public class HttpApiWOrgTerm
        {
            public string taxonomy { get; set; }
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class HttpApiWOrgMeta
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class Links
        {
            public List<Self> self { get; set; }
            public List<Collection> collection { get; set; }
            public List<Author> author { get; set; }
            public List<Reply> replies { get; set; }
        }
    }

}