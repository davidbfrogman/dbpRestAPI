using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class PortfolioBook
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageThumbnailURL { get; set; }
        public int Order { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public List<PortfolioItem> Items { get; set; }
        public string Class { get; set; }
        public string Subtitle { get; set; }
    }
}