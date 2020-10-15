using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EldaCPRFirstAidTraining.Data;

namespace EldaCPRFirstAidTraining.Pages.TrainingSchedule
{
    public class DetailsModel : PageModel
    {
        private readonly EldaCPRFirstAidTraining.Data.EldaCPRFirstAidTrainingContext _context;

        public DetailsModel(EldaCPRFirstAidTraining.Data.EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        public Data.TrainingSchedule TrainingSchedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrainingSchedule = await _context.TrainingSchedules
                .Include(t => t.Training).FirstOrDefaultAsync(m => m.Id == id);

            if (TrainingSchedule == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
