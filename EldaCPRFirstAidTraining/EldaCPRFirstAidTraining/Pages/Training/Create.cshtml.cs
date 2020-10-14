using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EldaCPRFirstAidTraining.Data;
using Microsoft.AspNetCore.Authorization;

namespace EldaCPRFirstAidTraining.Pages.Training
{
    //[Authorize]
    public class CreateModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public CreateModel(EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Data.Training Training { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Trainings.Add(Training);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
