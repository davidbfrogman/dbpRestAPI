using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dbpRestAPI.Models
{
    public class PortfolioItem
    {
        [Key]
        public int Id { get; set; }
        public int PortfolioBookId { get; set; }
        public string AltText { get; set; }
        public string ImageURL { get; set; }
        public int Order { get; set; }
    }
}