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
    public class IndexModel : PageModel
    {
        private readonly EldaCPRFirstAidTrainingContext _context;

        public IndexModel(EldaCPRFirstAidTrainingContext context)
        {
            _context = context;
        }

        public IList<Data.TrainingSchedule> TrainingSchedule { get;set; }

        public async Task OnGetAsync()
        {
            TrainingSchedule = await _context.TrainingSchedules
                .Include(t => t.Training).ToListAsync();
        }
    }
}
