using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using CsvHelper;
using System.Globalization;
using WindowsInput;
using WindowsInput.Native;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using HCMS.Models;
using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;

namespace BulkMailSender.Area.Customer.Controllers
{
    public class EmailObject
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        
        public EmailObject(string email, string subject, string message, string firstName)
        {
            Email = email;
            Subject = subject;
            Message = message;
            FirstName = firstName;
        }
    }

    [Area("Tools")]
    public class AdmissionsController : Controller
    {
        private readonly ILogger<AdmissionsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AdmissionsController(ILogger<AdmissionsController> logger,ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
        }



        public IActionResult AddMailAccount()
        {
            return View();
        }
        public IActionResult Product()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }


        public IActionResult Home()
        {
            // Return the index.html file as a view
            return View();
        }

        [HttpPost]
        public IActionResult SaveMailAccount(MailAccounts account)
        {
            if (ModelState.IsValid)
            {
                // Save the new mail account to the database
                _context.MailAccounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Compose");
            }
            return View(account);
        }


        [HttpGet]
        public IActionResult Compose()
        {
            // Retrieve mail accounts data from the database and pass it to the view
            var emailTemplates = _context.EmailTemplates.ToList();
            ViewBag.EmailTemplates = emailTemplates;
            var mailAccounts = _context.MailAccounts.ToList(); // Assuming MailAccounts is DbSet in your DbContext
            ViewBag.MailAccounts = mailAccounts;
            return View();
        }



        public IActionResult AddEmailTemplate()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SaveTemplate(EmailTemplates template)
        {
            if (ModelState.IsValid)
            {
                // Add the template to the database
                _context.EmailTemplates.Add(template);
                _context.SaveChanges();
                return RedirectToAction("Compose"); // Redirect to home page or any other appropriate action
            }
            return View(template);
        }


        [HttpPost]
        public IActionResult SendEmail(int accountId, string subject, string message, string recipients, IFormFile contactFile)
        {
            // Retrieve the selected MailAccount from the database
            var account = _context.MailAccounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null && !string.IsNullOrWhiteSpace(subject) &&
                !string.IsNullOrWhiteSpace(message) && !string.IsNullOrWhiteSpace(recipients))
            {
                // Read contacts from CSV file
                if (contactFile != null && contactFile.Length > 0)
                {
                    using (var reader = new StreamReader(contactFile.OpenReadStream()))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        // Read the CSV records and extract email addresses
                        var contacts = new List<EmailObject>();
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read() && contacts.Count < 301) // Limit to the first 301 contacts
                        {
                            EmailObject Eo = new EmailObject(csv.GetField<string>("Email"),"","",csv.GetField<string>("FirstName"));
                            contacts.Add(Eo);
                            //contacts.Add(csv.GetField<string>("Email"));
                        }

                        // Send emails to extracted email addresses
                        foreach (var email in contacts)
                        {
                            var mailMessage = new MimeMessage();
                            mailMessage.From.Add(new MailboxAddress(account.Email, account.Email));
                            mailMessage.To.Add(MailboxAddress.Parse(email.Email.Trim()));
                            mailMessage.Subject = subject;
                            // FirstName Code
                            var personalizedMessage = message.Replace("{{firstname}}", email.FirstName.ToString());
                            mailMessage.Body = new TextPart("plain")
                            {
                                Text = personalizedMessage
                            };
                            // var unsubscribeLink = $"http://yourdomain.com/unsubscribe?email={email}";
                            // var fullMessage = $"{message}<br><br><a href=\"{unsubscribeLink}\">Unsubscribe</a>";

                            using (var client = new SmtpClient())
                            {
                                client.Connect("smtp.gmail.com", 587, false); // Assuming Gmail SMTP settings
                                client.Authenticate(account.Email, account.Password);
                                client.Send(mailMessage);
                                client.Disconnect(true);
                            }
                        }
                    }
                }
                return RedirectToAction("Success");
            }
            // If any of the required fields are empty or if the account is not found, return to the compose view
            return RedirectToAction("Compose");
        }

        public IActionResult SendEmail(int accountId, string subject, string message, string recipients)
        {
            // Retrieve the selected MailAccount from the database
            var account = _context.MailAccounts.FirstOrDefault(a => a.Id == accountId);

            if (account != null && !string.IsNullOrWhiteSpace(subject) &&
                !string.IsNullOrWhiteSpace(message) && !string.IsNullOrWhiteSpace(recipients))
            {
                string[] recipientArray = recipients.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var recipient in recipientArray)
                {
                    var mailMessage = new MimeMessage();
                    mailMessage.From.Add(new MailboxAddress(account.Email, account.Email));
                    mailMessage.To.Add(MailboxAddress.Parse(recipient.Trim()));
                    mailMessage.Subject = subject;

                    mailMessage.Body = new TextPart("plain")
                    {
                        Text = message
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false); // Assuming Gmail SMTP settings
                        client.Authenticate(account.Email, account.Password);

                        client.Send(mailMessage);
                        client.Disconnect(true);
                    }
                }

                return RedirectToAction("Success");
            }

            // If any of the required fields are empty or if the account is not found, return to the compose view
            return RedirectToAction("Compose");
        }


        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }



}