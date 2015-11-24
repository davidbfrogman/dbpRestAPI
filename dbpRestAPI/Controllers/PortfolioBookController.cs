using dbpRestAPI.Controllers.Base;
using dbpRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dbpRestAPI.Controllers
{
    public class PortfolioBookController : dbpBaseController
    {
        // GET: api/PortfolioBook
        public IEnumerable<PortfolioBook> Get()
        {
            List<PortfolioBook> portfolios = new List<PortfolioBook>() {
                new PortfolioBook()
                {
                    id = 1,
                    Title = "Rebecca",
                    Description = "I had a blast shooting these",
                    Order = 2

                },
                new PortfolioBook()
                {
                    id = 2,
                    Title = "Liv",
                    Description = "Liv was super nice",
                    Order = 3
                }
            };

            return portfolios;
        }

        // GET: api/PortfolioBook/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PortfolioBook
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PortfolioBook/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PortfolioBook/5
        public void Delete(int id)
        {
        }
    }
}
