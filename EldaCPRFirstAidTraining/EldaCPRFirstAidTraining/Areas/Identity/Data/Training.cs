using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EldaCPRFirstAidTraining.Data
{
    public class Training
    {
        public Training()
        {
            TrainingSchedules = new HashSet<TrainingSchedule>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public ICollection<TrainingSchedule> TrainingSchedules { get; set; }
    }
}
