using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NemesisGroup.Controllers
{
    public class EmailForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact(EmailForm form)
        {
            MailMessage message = new MailMessage();
            message.To.Add("nemesistrnggroup@gmail.com");
            message.Subject = "Web Contact Form - " + form.Name;
            message.From = new MailAddress("contact@nemesisgroupllc.com");
            message.Body = "Request Form from: " + form.Name + "<br />Email: " + form.Email + "<br />Message: " + form.Message;
            message.ReplyToList.Add(form.Email);
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            SmtpClient client = new SmtpClient("smtpout.secureserver.net", 25);
            client.UseDefaultCredentials = false;
            
            // make sure that the password is missing when you check in changes to the Git repo.
            // TODO: Add password
            string password = "";
            
            client.Credentials = new NetworkCredential("contact@nemesisgroupllc.com", password);
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(message);
            return RedirectToAction("Index");
        }
    }
}
