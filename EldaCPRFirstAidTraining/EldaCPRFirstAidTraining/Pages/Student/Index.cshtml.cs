using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EldaCPRFirstAidTraining.Data;

namespace EldaCPRFirstAidTraining.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public IndexModel(EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        public IList<Data.Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students
                .Include(s => s.TrainingSchedule.Training).ToListAsync();
        }
    }
}
