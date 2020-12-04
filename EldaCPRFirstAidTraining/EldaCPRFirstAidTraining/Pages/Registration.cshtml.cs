using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EldaCPRFirstAidTraining.Data;
using EldaCPRFirstAidTraining.Utilities;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace EldaCPRFirstAidTraining.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RegistrationModel(EldaCPRFirstAidTrainingContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["ScheduleId"] = new SelectList(GetSchedule(), "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Data.Student Student { get; set; }

        [TempData]
        public string Message { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Student.CreatedOn = DateTime.Now;
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            var schedule = _context.TrainingSchedules.FirstOrDefault(ts => ts.Id == Student.ScheduleId);
            var training = _context.Trainings.FirstOrDefault(t => t.Id == schedule.TrainingId);

            var logoPath = Path.Combine(_hostingEnvironment.WebRootPath, $"assets/images/b-heart.png");

            SendEmailFromGmail sfgmail = new SendEmailFromGmail();
            sfgmail.SendEmail(Student.Email, Student.FristName + " " + Student.LastName, "Elda CPR and First Aid Training Confirmation",
                    string.Format("Dear " + Student.FristName + ", <br/> Thanks for registering to for the follwoing training: " + training.Title + " - " + training.Description + "; scheduled for " + schedule.StartDate + ". <br/>Please bring $" + training.Cost + " cash on the day of training. <br/><br/> We will follow up with you soon, <br/> Elda CPR and First Aid Training.<br/><br/>"), null);

            Message = $"Thanks for registering for {training.Title + " - " + training.Description} scheduled on {schedule.StartDate}.";

            return RedirectToPage("./Index");
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

}
