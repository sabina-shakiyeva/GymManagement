using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Concrete
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int WorkoutDayId { get; set; }
        public WorkoutDay WorkoutDay { get; set; }

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }

        public int Repetitions { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
