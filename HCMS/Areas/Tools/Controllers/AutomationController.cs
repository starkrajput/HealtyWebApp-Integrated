using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HCMS.Models;
using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using WindowsInput.Native;
using WindowsInput;
using System.Windows.Forms;


namespace BulkMailSender.Area.Customer.Controllers
{
    public class AutomationObject
    {
        public string Type { get; set; }
        public int ClientX { get; set; }
        public int ClientY { get; set; }
        public int KeyCode { get; set; }
        public string Key { get; set; }
        public AutomationObject(string type, int clientX, int clientY, int keyCode, string key)
        {
            Type = type;
            ClientX = clientX;
            ClientY = clientY;
            KeyCode = keyCode;
            Key = key;
        }
    }

    [Area("Tools")]
    public class AutomationController : Controller
    {
        private readonly ILogger<AutomationController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private List<AutomationObject> recordedActions;
        private int sequence;
        private readonly IInputSimulator _inputSimulator;
        public AutomationController(ILogger<AutomationController> logger, ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
            recordedActions = new List<AutomationObject>();
            sequence = 1;
            _inputSimulator = new InputSimulator();
        }
        
        public IActionResult Compose() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> StartRecording()
        {
            // Logic to start recording (handled in JavaScript)
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> StopRecording()
        {
            // Logic to stop recording (handled in JavaScript)
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RepeatAutomation(string recordedEvents)
        {
            var recordedEventsList = JsonConvert.DeserializeObject<List<AutomationObject>>(recordedEvents);
            Console.WriteLine(recordedEvents);
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Calculate scaling factors
            double scaleX = 65535.0 / screenWidth;
            double scaleY = 63535.0 / screenHeight;
            // For demonstration purposes, log each event to the console
            foreach (var ev in recordedEventsList)
            {
                Console.WriteLine($"Type: {ev.Type}, ClientX: {ev.ClientX}, ClientY: {ev.ClientY}, KeyCode: {ev.KeyCode}, Key: {ev.Key}");
                if (ev.Type == "Mouse")
                {
                    // Simulate mouse move and click
                    double normalizedX = ev.ClientX * scaleX;
                    double normalizedY = ev.ClientY * scaleY;

                    // Simulate mouse move and click
                    _inputSimulator.Mouse.MoveMouseToPositionOnVirtualDesktop(normalizedX, normalizedY).LeftButtonClick();

                }
                else if (ev.Type == "Key")
                {
                    // Simulate key press
                    _inputSimulator.Keyboard.KeyPress((VirtualKeyCode)ev.KeyCode);
                }
                Thread.Sleep(9000);
            }
            Thread.Sleep(100000);

            return RedirectToAction("Index");
        }

       
    }

  

}