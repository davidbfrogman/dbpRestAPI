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
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}