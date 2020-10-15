using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EldaCPRFirstAidTraining.Data;
using EldaCPRFirstAidTraining.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace EldaCPRFirstAidTraining.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EldaCPRFirstAidTrainingContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DateTime UpComingTraining { get; set; }

        [BindProperty]
        public Student Student { get; set; }

        [TempData]
        public string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger, EldaCPRFirstAidTrainingContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostingEnvironment = hostEnvironment;
        }

        public void OnGet()
        {
            var training = _context.TrainingSchedules.OrderBy(t => t.StartDate).FirstOrDefault();

            if (training != null && training.StartDate != null)
                UpComingTraining = training.StartDate.Date;
            else
                UpComingTraining = DateTime.Now.Date;

            ViewData["TrainingId"] = new SelectList(GetSchedule(), "Id", "Title");
        }

        public IActionResult OnGetFindAllEvents()
        {
            var events = _context.TrainingSchedules.Select(e => new
            {
                id = e.Id,
                title = e.Training.Title,
                description = e.Training.Description + ". $" + e.Training.Cost,
                start = e.StartDate,
                end = e.EndDate
            }).ToList();
            return new JsonResult(events);
        }

        public IActionResult OnPostAddTraining()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Student.CreatedOn = DateTime.Now;

            _context.Students.Add(Student);
            _context.SaveChanges();


            var logoPath = Path.Combine(_hostingEnvironment.WebRootPath, $"assets/images/NH-Logo.png");

            //SendEmailFromGmail sfgmail = new SendEmailFromGmail();
            //sfgmail.SendEmail(Student.Email, Student.FristName + " " + Student.LastName, "Elda CPR and First Aid Training Confirmation",
            //        string.Format("Dear " + Student.FristName + ", <br/> Thanks for registering to for our " + myEvent.Name + " on " + date + " from " + myEvent.StartTime + " to " + myEvent.EndTime + ". <br/> We will follow up with you soon. <br/><br/> Nourishing Hands, Inc.<br/><br/>"), logoPath);

            return RedirectToPage("./Index");
        }

        private IList<Schedule> GetSchedule()
        {
            IList<Schedule> sch = _context.TrainingSchedules.Select(e => new Schedule
            {
                Id = e.Id,
                Title = $"{e.Training.Title}: {e.Training.Description } -${e.Training.Cost} on {e.StartDate.ToString("MM/dd/yy")}"
            }).ToList();

            return sch;
        }

    }

    public class Schedule 
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

}
