using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EldaCPRFirstAidTraining.Data;

namespace EldaCPRFirstAidTraining.Pages.Training
{
    public class DeleteModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public DeleteModel(EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Training Training { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Training = await _context.Trainings.FirstOrDefaultAsync(m => m.Id == id);

            if (Training == null)
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

            Training = await _context.Trainings.FindAsync(id);

            if (Training != null)
            {
                _context.Trainings.Remove(Training);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
