using System.Web.Mvc;
using StoreFront.Models;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System;

namespace MVC2EFSecured.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //In order to get our validation to work, we need to check the ModelState
            if (!ModelState.IsValid)
            {
                return View(cvm);//Passing in cvm will populate the form with all the user typed in before they submitted the request.
            }
            //Contact functionality below
            //Build the message
            string message = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

            //MailMessage object - what sends the mail
            MailMessage mm = new MailMessage(
                //From
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                //To
                ConfigurationManager.AppSettings["EmailTo"].ToString(),
                //Subject
                cvm.Subject,
                //Body of message
                message
                );

            //Configure certain properties of the MailMessage object
            //Allow Html in formatting
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;

            //Respond to the email given in the form instead of our smarterasp email
            mm.ReplyToList.Add(cvm.Email);

            //Assemble the SmtpClient - the vehicle by which the message is sent
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Add the credentials to the SmtpClient
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString()
                );

            //Try to send the email
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {

                ViewBag.CustomerMessage = $"we are sorry the request could not be sent at this time. Please try again later.<br/>Error Message:" +
                    $" <br/> {ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirm", cvm);
           
        }

        [HttpGet]
        public ActionResult Shop()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShopSingle()
        {
            return View();
        }
    }
}
