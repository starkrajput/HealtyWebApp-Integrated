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
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace BulkMailSender.Area.Customer.Controllers
{
    public class IGObject
    {
        public string Username { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public IGObject(string username, string subject, string message, string firstName)
        {
            Username = username;
            Subject = subject;
            Message = message;
            FirstName = firstName;
        }
    }

    [Area("Tools")]
    public class IGController : Controller
    {
        private readonly ILogger<IGController> _logger;
        private readonly ApplicationDbContext _context;
        public IGController(ILogger<IGController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult AddIGAccount()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveIGAccount(IGAccounts account)
        {
            if (ModelState.IsValid)
            {
                // Save the new mail account to the database
                _context.IGAccounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Compose");
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Compose()
        {
            // Retrieve mail accounts data from the database and pass it to the view
            var IGTemplates = _context.IGTemplates.ToList();
            ViewBag.IGTemplates = IGTemplates;
            var IGAccounts = _context.IGAccounts.ToList(); // Assuming MailAccounts is DbSet in your DbContext
            ViewBag.IGAccounts = IGAccounts;
            return View();
        }
        public IActionResult AddIGTemplate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveTemplate(IGTemplates template)
        {
            if (ModelState.IsValid)
            {
                // Add the template to the database
                _context.IGTemplates.Add(template);
                _context.SaveChanges();
                return RedirectToAction("Compose"); // Redirect to home page or any other appropriate action
            }
            return View(template);
        }
        [HttpPost]
        
        public async Task<IActionResult> SendDMs(int accountId, string subject, string message, string recipients, IFormFile contactFile)
        {
            var account = _context.IGAccounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null && !string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(message) && contactFile != null && contactFile.Length > 0)
            {
                var userSession = new UserSessionData
                {
                    UserName = account.Username,
                    Password = account.Password
                };

                var api = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .Build();
               
                var loginResult = await api.LoginAsync();
                if (!loginResult.Succeeded)
                {
                    // Authentication failed, handle the error
                    return RedirectToAction("Error");
                }
               
                // Read contacts from CSV file
                using (var reader = new StreamReader(contactFile.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var contacts = new List<IGObject>();
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read() && contacts.Count < 301) // Limit to the first 301 contacts
                    {
                        IGObject igObject = new IGObject(csv.GetField<string>("ProfileURL"), "", "", csv.GetField<string>("FirstName"));
                        contacts.Add(igObject);
                      
                    }

                    // Send DMs to extracted profile URLs
                    foreach (var contact in contacts)
                    {
                        var recipientUsername = contact.Username; // Assuming the email field contains the profile URL
                        var dmText = message.Replace("{{firstname}}", contact.FirstName.ToString());
                        //var match = Regex.Match(recipientProfileUrl, @"instagram\.com\/(?<username>[a-zA-Z0-9\._]+)\/?");
                        //var username = "";
                        if (true)
                        {
                          //  username = match.Groups["username"].Value;
                            var userSearchResult = await api.UserProcessor.GetUserAsync(recipientUsername);
                            
                            if (userSearchResult.Succeeded)
                            {

                                var userId = userSearchResult.Value.Pk;
                                var dmResult = await api.MessagingProcessor.SendDirectTextAsync(userId.ToString(), "",dmText);
                                if (dmResult.Succeeded)
                                {
                                    // DM sent successfully
                                    Console.WriteLine("Here we are");
                                }
                                else
                                {
                                    var errorMessage = $"Failed to send DM to user . Reason: {dmResult.Info.Message}";

                                }
                            }
                            else
                            {
                                var errorMessage = $"Failed to get user ID for username. Reason: {userSearchResult.Info.Message}";

                            }
                        }
                        else
                        {
                            var errorMessage = $"An error occurred while sending DM to user . Exception: ";
                        }
                    }
                }

                await api.LogoutAsync(); // Logout after sending DMs
                return RedirectToAction("Success");
            }
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