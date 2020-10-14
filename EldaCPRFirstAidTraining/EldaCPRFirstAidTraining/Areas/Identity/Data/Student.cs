using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EldaCPRFirstAidTraining.Data
{
    public class Student
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public virtual TrainingSchedule TrainingSchedule { get; set; }
    }
}
