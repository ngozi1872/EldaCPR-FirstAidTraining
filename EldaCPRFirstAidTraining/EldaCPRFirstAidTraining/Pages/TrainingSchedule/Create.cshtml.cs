using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EldaCPRFirstAidTraining.Data;

namespace EldaCPRFirstAidTraining.Pages.TrainingSchedule
{
    public class CreateModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public CreateModel(EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TrainingId"] = new SelectList(_context.Trainings, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Data.TrainingSchedule TrainingSchedule { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TrainingSchedules.Add(TrainingSchedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

       
    }
}
