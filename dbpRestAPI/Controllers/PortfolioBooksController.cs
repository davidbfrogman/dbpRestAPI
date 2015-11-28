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
            //List<PortfolioBook> portfolios = new List<PortfolioBook>() {
            //    new PortfolioBook()
            //    {
            //        Title = "Antoinette",
            //        Description = "I had a blast shooting these",
            //        ImageThumbnailURL = "http://www.davebrownphotography.com/Images/Fashion/_DSC1141.jpg",
            //        Order = 2,
            //        Items = new List<PortfolioItem>()
            //        {
            //            new PortfolioItem()
            //            {
            //                AltText = "This is some alt text",
            //                Order = 1,
            //                ImageURL = "DSC_123"
            //            }
            //        }

            //    },
            //    new PortfolioBook()
            //    {
            //        Title = "Savage",
            //        Description = "Savage was super nice",
            //        ImageThumbnailURL = "http://www.davebrownphotography.com/Images/Fashion/01ABDSC9151.jpg",
            //        Order = 3
            //    }
            //};

            //foreach (var book in portfolios)
            //{
            //    db.PortfolioBooks.Add(book);
            //}

            return db.PortfolioBooks.Include(pb => pb.Items).OrderBy(book => book.Id);
        }

        // GET: api/PortfolioBooks/5
        [ResponseType(typeof(PortfolioBook))]
        public IHttpActionResult GetPortfolioBook(int Id)
        {
            //db.PortfolioBooks.Include(pb => pb.Items).
            PortfolioBook portfolioBook = db.PortfolioBooks.Find(Id);
            db.Entry(portfolioBook).Collection(pb => pb.Items).Load();

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
                db.SaveChanges();
                db.Entry(portfolioBook).Collection(pb => pb.Items).Load();

                List<PortfolioItem> tempPortfolioItems = new List<PortfolioItem>();
                foreach (var item in portfolioBook.Items)
                {
                    tempPortfolioItems.Add(item);
                }

                foreach (var item in tempPortfolioItems)
                {
                    var portItemToDelete = db.PortfolioItems.Where(t => t.Id == item.Id).FirstOrDefault();
                    if (portItemToDelete != null)
                    {
                        //update case
                        db.PortfolioItems.Attach(item);
                        db.Entry(portItemToDelete).State = EntityState.Modified;
                    }
                    else
                    {
                        //adding
                        db.PortfolioItems.Add(item);
                    }
                }
                db.SaveChanges();
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
            db.Entry(portfolioBook).Collection(pb => pb.Items).Load();

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