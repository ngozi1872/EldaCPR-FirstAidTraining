using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldaCPRFirstAidTraining.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace EldaCPRFirstAidTraining.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EldaCPRFirstAidTrainingContext _context;

        public DateTime UpComingTraining { get; set; }

        public IndexModel(ILogger<IndexModel> logger, EldaCPRFirstAidTrainingContext context)
        {
            _logger = logger;
            _context = context;

            var training = _context.TrainingSchedules.OrderBy(t => t.StartDate).FirstOrDefault();

            if (training != null && training.StartDate != null)
                UpComingTraining = training.StartDate.Date;
            else
                UpComingTraining = DateTime.Now.Date;

        }

        public void OnGet()
        {

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
    }
}
