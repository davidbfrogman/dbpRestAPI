using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using dbpRestAPI.Models;
using dbpRestAPI.Controllers.Base;
using dbpRestAPI.DataLayer;

namespace dbpRestAPI.Controllers
{
    public class PortfolioBooksController : dbpBaseController
    {
        // GET: api/PortfolioBooks
        public IQueryable<PortfolioBook> GetPortfolioBooks()
        {
            //Adding some seed data
            List<PortfolioBook> portfolios = new List<PortfolioBook>() {
                new PortfolioBook()
                {
                    Title = "Rebecca",
                    Description = "I had a blast shooting these",
                    ImageThumbnailURL = "http://www.davebrownphotography.com/Images/Fashion/_DSC1141.jpg",
                    Order = 2

                },
                new PortfolioBook()
                {
                    Title = "Liv",
                    Description = "Liv was super nice",
                    ImageThumbnailURL = "http://www.davebrownphotography.com/Images/Fashion/01ABDSC9151.jpg",
                    Order = 3
                }
            };

            foreach (var book in portfolios)
            {
                db.PortfolioBooks.Add(book);
            }

            return db.PortfolioBooks.OrderBy(book => book.Id);
        }

        // GET: api/PortfolioBooks/5
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult GetPortfolioBook(int Id)
        {
            PortfolioBook portfolioBook = db.PortfolioBooks.Find(Id);

            if (portfolioBook == null)
            {
                return NotFound();
            }

            return Ok(portfolioBook);
        }

        // PUT: api/PortfolioBooks/5 updating
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPortfolioBook(int Id, PortfolioBook portfolioBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != portfolioBook.Id)
            {
                return BadRequest();
            }

            try
            {
                db.PortfolioBooks.Attach(portfolioBook);
                db.Entry(portfolioBook).State = EntityState.Modified;

                return StatusCode(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                //There was a problem storing the portfolio
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/PortfolioBooks adding
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult PostPortfolioBook(PortfolioBook portfolioBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PortfolioBooks.Add(portfolioBook);

            return CreatedAtRoute("DefaultApi", new { Id = portfolioBook.Id }, portfolioBook);
        }

        // DELETE: api/PortfolioBooks/5
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult DeletePortfolioBook(int Id)
        {
            var item = db.PortfolioBooks.Where(t => t.Id == Id).FirstOrDefault();
            if (item != null)
            {
                db.PortfolioBooks.Remove(item);
            }
            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}