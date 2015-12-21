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
            IQueryable<PortfolioBook> books =  db.PortfolioBooks.Include(pb => pb.Items).OrderBy(book => book.Id);
            foreach (var book in books)
            {
                book.Items = book.Items.OrderBy(item => item.Order).ToList();
            }
            return books;
        }

        [Route("api/GetPortfolioCategories")]
        public List<PortfolioCategory> GetPortfolioCategories()
        {
            List<PortfolioCategory> categories = new List<PortfolioCategory>();
            IQueryable<PortfolioBook> books = db.PortfolioBooks;

            categories.Add(new PortfolioCategory() { Name = "All", Count = books.Count(), Filter = "*", Order=0 });

            foreach (var book in books)
            {
                //if we already have a category created, then we need to add to the count.
                if (categories.Where(cc => cc.Name == book.Category).Count() > 0)
                {
                    categories.Where(cc => cc.Name == book.Category).First().Count++;
                }
                else
                {
                    categories.Add(new PortfolioCategory() { Name = book.Category, Count = 1, Filter = "." + book.Category });
                }
            }
            
            return categories;
        }

        [Route("api/GetPortfolioBooksForDisplay")]
        public IQueryable<PortfolioBook> GetPortfolioBooksForDisplay()
        {
            return db.PortfolioBooks.Where(book=> book.IsActive == true).Include(pb => pb.Items).OrderBy(book => book.Order);
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

            portfolioBook.Items = portfolioBook.Items.OrderBy(item => item.Order).ToList();

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

                //Create a new temporary list to store the items.
                List<PortfolioItem> tempPortfolioItems = new List<PortfolioItem>();
                foreach (var item in portfolioBook.Items)
                {
                    tempPortfolioItems.Add(item);
                }

                //Go through the temporary list, figuring out if we need to add 
                //or update the item.
                foreach (var item in tempPortfolioItems)
                {
                    var portItemToDelete = db.PortfolioItems.Where(t => t.Id == item.Id).FirstOrDefault();
                    //If we found it in the database we need to update it.
                    if (portItemToDelete != null)
                    {
                        //update case
                        db.PortfolioItems.Attach(item);
                        db.Entry(portItemToDelete).State = EntityState.Modified;
                    }
                    //Otherwise we're going to add it.
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
            //Whenever we're adding there aren't any new items to load.
            //db.Entry(portfolioBook).Collection(pb => pb.Items).Load();

            return StatusCode(HttpStatusCode.Accepted);
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