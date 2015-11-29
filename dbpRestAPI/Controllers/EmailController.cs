using dbpRestAPI.Controllers.Base;
using dbpRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace dbpRestAPI.Controllers
{
    public class EmailController : dbpBaseController
    {
        // POST: api/Email
        public IHttpActionResult Post(dbpEmail postedEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                MailMessage email = new MailMessage(postedEmail.address, "info@davebrownphotography.com", "Inquiry Email [ " + postedEmail.name + " ] Budget: " + postedEmail.budget, postedEmail.message);
                email.ReplyToList.Add(new MailAddress(postedEmail.address));

                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], 587);
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUser"], ConfigurationManager.AppSettings["SmtpPassword"]);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtpClient.Send(email);

                return Created<dbpEmail>("dbpEmailController", postedEmail);
            }
            catch(Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
