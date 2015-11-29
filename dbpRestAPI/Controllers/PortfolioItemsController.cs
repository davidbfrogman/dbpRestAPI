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
using dbpRestAPI.DataLayer;
using dbpRestAPI.Models;

namespace dbpRestAPI.Controllers
{
    public class PortfolioItemsController : ApiController
    {
        private dbpDatabaseContext db = new dbpDatabaseContext();

        // GET: api/PortfolioItems
        public IQueryable<PortfolioItem> GetPortfolioItems()
        {
            return db.PortfolioItems;
        }

        // GET: api/PortfolioItems/5
        [ResponseType(typeof(PortfolioItem))]
        public IHttpActionResult GetPortfolioItem(int id)
        {
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return Ok(portfolioItem);
        }

        // PUT: api/PortfolioItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPortfolioItem(int id, PortfolioItem portfolioItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != portfolioItem.Id)
            {
                return BadRequest();
            }

            db.Entry(portfolioItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PortfolioItems
        [ResponseType(typeof(PortfolioItem))]
        public IHttpActionResult PostPortfolioItem(PortfolioItem portfolioItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PortfolioItems.Add(portfolioItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = portfolioItem.Id }, portfolioItem);
        }

        // DELETE: api/PortfolioItems/5
        [ResponseType(typeof(PortfolioItem))]
        public IHttpActionResult DeletePortfolioItem(int id)
        {
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            db.PortfolioItems.Remove(portfolioItem);
            db.SaveChanges();

            return Ok(portfolioItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PortfolioItemExists(int id)
        {
            return db.PortfolioItems.Count(e => e.Id == id) > 0;
        }
    }
}