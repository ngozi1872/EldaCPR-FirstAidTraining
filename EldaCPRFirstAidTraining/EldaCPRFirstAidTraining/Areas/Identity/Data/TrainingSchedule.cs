using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EldaCPRFirstAidTraining.Data
{
    public class TrainingSchedule
    {
        public TrainingSchedule()
        {
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public ICollection<Student> Students { get; set; }

        [ForeignKey(nameof(TrainingId))]
        public virtual Training Training { get; set; }
    }
}
