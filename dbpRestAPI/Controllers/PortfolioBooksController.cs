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
                DocumentSession.Store(book);
                DocumentSession.SaveChanges();
            }

            return base.DocumentSession.Query<PortfolioBook>();
        }

        // GET: api/PortfolioBooks/5
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult GetPortfolioBook(string Id)
        {
            PortfolioBook portfolioBook = DocumentSession.Load<PortfolioBook>(Id);
            if (portfolioBook == null)
            {
                return NotFound();
            }

            return Ok(portfolioBook);
        }

        // PUT: api/PortfolioBooks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPortfolioBook(string Id, PortfolioBook portfolioBook)
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
                DocumentSession.Store(portfolioBook, Id);
                DocumentSession.SaveChanges();
                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //There was a problem storing the portfolio
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/PortfolioBooks
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult PostPortfolioBook(PortfolioBook portfolioBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocumentSession.Store(portfolioBook);
            DocumentSession.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { Id = portfolioBook.Id }, portfolioBook);
        }

        // DELETE: api/PortfolioBooks/5
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult DeletePortfolioBook(string Id)
        {
            PortfolioBook portfolioBook = DocumentSession.Load<PortfolioBook>(Id);
            if (portfolioBook == null)
            {
                return NotFound();
            }

            DocumentSession.Delete<PortfolioBook>(portfolioBook);
            DocumentSession.SaveChanges();

            return Ok(portfolioBook);
        }
    }
}