using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public Data.Student Student { get; set; }

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Student.CreatedOn = DateTime.Now;

                _context.Students.Add(Student);
                _context.SaveChanges();

                var schedule = _context.TrainingSchedules.FirstOrDefault(ts => ts.Id == Student.ScheduleId);
                var training = _context.Trainings.FirstOrDefault(t => t.Id == schedule.TrainingId);

                var logoPath = Path.Combine(_hostingEnvironment.WebRootPath, $"assets/images/b-heart.png");

                SendEmailFromGmail sfgmail = new SendEmailFromGmail();
                sfgmail.SendEmail(Student.Email, Student.FristName + " " + Student.LastName, "Elda CPR and First Aid Training Confirmation",
                        string.Format("Dear " + Student.FristName + ", <br/> Thanks for registering to for the follwoing training: " + training.Title + " - " + training.Description + "; scheduled for " + schedule.StartDate + ". <br/>Please bring $" + training.Cost + " cash on the day of training. <br/><br/> We will follow up with you soon, <br/> Elda CPR and First Aid Training.<br/><br/>"), null);

                Message = $"Thanks for registering for {training.Title + " - " + training.Description} scheduled on {schedule.StartDate}.";
                return RedirectToPage("./Index");

            }
            catch(Exception ex)
            {
                Message = $"Error: {ex.Message}";
                return RedirectToPage("./Index");
            }
        }

        private IList<Schedule> GetSchedule()
        {
            IList<Schedule> sch = _context.TrainingSchedules.Select(e => new Schedule
            {
                Id = e.Id,
                Title = $"{e.Training.Title}: {e.Training.Description } - ${e.Training.Cost} on {e.StartDate.ToString("MM/dd/yy")}"
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
