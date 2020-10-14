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
    public class DeleteModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public DeleteModel(EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrainingSchedule = await _context.TrainingSchedules.FindAsync(id);

            if (TrainingSchedule != null)
            {
                _context.TrainingSchedules.Remove(TrainingSchedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
