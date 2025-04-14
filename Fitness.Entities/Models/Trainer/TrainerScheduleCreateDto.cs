using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Models.Trainer
{
    public class TrainerScheduleCreateDto
    {
        public int TrainerId { get; set; }
        public int? UserId { get; set; }      // Fərdi dərs üçün
        public int? GroupId { get; set; }     // Qrup dərs üçün
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
