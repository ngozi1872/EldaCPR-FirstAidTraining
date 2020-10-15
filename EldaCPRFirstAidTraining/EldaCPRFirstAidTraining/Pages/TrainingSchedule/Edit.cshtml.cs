using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EldaCPRFirstAidTraining.Data;

namespace EldaCPRFirstAidTraining.Pages.TrainingSchedule
{
    public class EditModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public EditModel(EldaCPRFirstAidTrainingContext context)
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
           ViewData["TrainingId"] = new SelectList(_context.Trainings, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TrainingSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingScheduleExists(TrainingSchedule.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TrainingScheduleExists(int id)
        {
            return _context.TrainingSchedules.Any(e => e.Id == id);
        }
    }
}
