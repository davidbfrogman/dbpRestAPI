using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Controllers;

namespace dbpRestAPI.Controllers.Base
{
    public class dbpBaseController : ApiController
    {
        public IDocumentSession DocumentSession { get; set; }
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            this.DocumentSession = RavenDbConfig.Store.OpenSession();
            base.Initialize(controllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.DocumentSession != null)
                this.DocumentSession.SaveChanges();
            this.DocumentSession.Dispose();
            base.Dispose(disposing);
        }
    }
}